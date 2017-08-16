using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Messengers;
using UnityEngine;
using Utils.Net;
using Utils.Net.Conversion;
using Utils.Net.IO;
using Utils.Singleton;
using War;


namespace Net {

    public class WSConnection : MonoBehaviour {

        private readonly ByteBuffer _bb = new ByteBuffer();

        private WebSocket _socket;

        private int _turnDataRead;

        public PlayerInfoReceivedMessenger OnPlayerInfo { get; private set; }
        public HubChangedMessenger OnHubChanged { get; private set; }
        public StartGameMessenger OnStartGame { get; private set; }
        public PlayerQuitMessenger OnPlayerQuit { get; private set; }
        public PlayerWinMessenger OnPlayerWin { get; private set; }
        public TurnDataReceivedMessenger OnTurnData { get; private set; }
        public NoWinnerMessenger OnNoWinner { get; private set; }
        public NewTurnMessenger OnNewTurn { get; private set; }


        private void Awake () {
            The<WSConnection>.Set(this);

            OnPlayerInfo = new PlayerInfoReceivedMessenger();
            OnHubChanged = new HubChangedMessenger();
            OnStartGame = new StartGameMessenger();
            OnPlayerQuit = new PlayerQuitMessenger();
            OnPlayerWin = new PlayerWinMessenger();
            OnTurnData = new TurnDataReceivedMessenger();
            OnNoWinner = new NoWinnerMessenger();
            OnNewTurn = new NewTurnMessenger();
        }


        public void Work () {}


        public void Update () {
            if (_socket == null) return;

            for (_turnDataRead = 0; _turnDataRead < 2; ) {
                var bytes = _socket.Recv();
                if (bytes == null) break;
                // todo: move logging to Parse method
                Parse(bytes);
            }
        }


        private void OnDisable () {
            if (_socket == null) return;
            _socket.Close();
            _socket = null;
        }


        // todo: replace this with commands! 
        private void Parse (byte[] bytes) {
            var stream = new MemoryStream(bytes);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

            switch (reader.ReadByte()) {
                case ServerAPI.AccountData:
                    OnPlayerInfo.Send(new PlayerInfo(reader.ReadInt32()));
                    break;

                case ServerAPI.HubChanged:
                    OnHubChanged.Send(reader.ReadByte(), reader.ReadByte());
                    break;

                case ServerAPI.StartGame:
                    int seed = reader.ReadInt32();
                    var players = new List<int>();
                    for (byte i = 0, end = reader.ReadByte(); i < end; ++i) players.Add(reader.ReadInt32());
                    OnStartGame.Send(new GameInitData(seed, players));
                    break;

                case ServerAPI.LeftGame:
                    OnPlayerQuit.Send(reader.ReadInt32());
                    break;

                case ServerAPI.ShowWinner:
                    OnPlayerWin.Send(reader.ReadInt32());
                    break;

                case ServerAPI.TurnData:
                    ++_turnDataRead;
                    var td = new TurnData(reader.ReadByte(), reader.ReadSingle(), reader.ReadSingle());
                    OnTurnData.Send(td);
                    break;

                case ServerAPI.NoWinner:
                    OnNoWinner.Send();
                    break;

                case ServerAPI.NewTurn:
                    OnNewTurn.Send(reader.ReadInt32());
                    break;

                default: throw new NotImplementedException();
            }
            reader.Close();
            stream.Close();
        }


        public void Authorize (string ip, int id) {
            StartCoroutine(AuthCoroutine(ip, id));
        }


        private IEnumerator AuthCoroutine (string ip, int id) {
            if (_socket != null) {
                _socket.Close();
                Debug.Log("TRYING TO OPEN SECOND CONNECTION");
            }
            _socket = new WebSocket(new Uri("ws://localhost:8080/events/"));
            yield return StartCoroutine(_socket.Connect());

            StartCoroutine(SendPing());

            _bb.Clear();
            _bb.WriteByte(ClientAPI.Auth);
            _bb.WriteInt32(id);
            _socket.Send(_bb);
        }


        private IEnumerator SendPing () {
            while (_socket != null) {
                yield return new WaitForSecondsRealtime(60);
                _socket.Send(new byte[0]);
            }
        }


        public void SendHubId (int id) {
            _bb.Clear();
            _bb.WriteByte(ClientAPI.ToHub);
            _bb.WriteByte((byte) id);
            _socket.Send(_bb);
        }


        public void SendTurnData (TurnData td) {
            _bb.Clear();
            _bb.WriteByte(ClientAPI.TurnData);
            _bb.WriteByte(td.Flags);
            _bb.WriteFloat(td.XY.X);
            _bb.WriteFloat(td.XY.Y);
            _socket.Send(_bb);
        }


        public void SendEndTurn (bool alive) {
            _bb.Clear();
            _bb.WriteByte(ClientAPI.EndTurn);
            _bb.WriteByte((byte) (alive ? 1 : 0));
            _socket.Send(_bb);
        }

    }

}
