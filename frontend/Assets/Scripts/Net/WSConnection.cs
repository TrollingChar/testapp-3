using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;
using Utils;
using Utils.Conversion;
using Utils.IO;
using War;


namespace Net {

    public class WSConnection : MonoBehaviour {

        [SerializeField] private UnityEvent_int onAccountData;
        [SerializeField] private UnityEvent_int_int onChangeHub;
        [SerializeField] private UnityEvent_GameData onStartGame;
        [SerializeField] private UnityEvent_int onPlayerQuit;
        [SerializeField] private UnityEvent_int onPlayerWin;
        [SerializeField] private UnityEvent_TurnData onTurnData;
        [SerializeField] private UnityEvent onNoWinner;
        [SerializeField] private UnityEvent_int onNewTurn;

        private WebSocket socket;
        private ByteBuffer bb = new ByteBuffer();

        private int turnDataRead;


        public void Work () {
            if (socket == null) return;

            turnDataRead = 0;
            for (byte[] bytes = socket.Recv();
                bytes != null && turnDataRead < 2;
                bytes = socket.Recv()
            ) {
                Debug.Log(BitConverter.ToString(bytes));
                Parse(bytes);
            }
        }


        private void OnDisable () {
            if (socket == null) return;
            socket.Close();
            socket = null;
        }


        private void Parse (byte[] bytes) {
            var stream = new MemoryStream(bytes);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

            switch (reader.ReadByte()) {
                case ServerAPI.AccountData:
                    onAccountData.Invoke(reader.ReadInt32());
                    break;
                case ServerAPI.HubChanged:
                    onChangeHub.Invoke(reader.ReadByte(), reader.ReadByte());
                    break;
                case ServerAPI.StartGame:
                    int seed = reader.ReadInt32();
                    var players = new List<int>();
                    for (byte i = 0, end = reader.ReadByte(); i < end; ++i) players.Add(reader.ReadInt32());
                    onStartGame.Invoke(new GameData(seed, players));
                    break;
                case ServerAPI.LeftGame:
                    onPlayerQuit.Invoke(reader.ReadInt32());
                    break;
                case ServerAPI.ShowWinner:
                    onPlayerWin.Invoke(reader.ReadInt32());
                    break;
                case ServerAPI.TurnData:
                    ++turnDataRead;
                    var td = new TurnData(reader.ReadByte(), reader.ReadSingle(), reader.ReadSingle());
                    onTurnData.Invoke(td);
                    break;
                case ServerAPI.NoWinner:
                    onNoWinner.Invoke();
                    break;
                case ServerAPI.NewTurn:
                    onNewTurn.Invoke(reader.ReadInt32());
                    break;
            }
            reader.Close();
            stream.Close();
        }


        public void Authorize (string ip, int id) {
            StartCoroutine(AuthCoroutine(ip, id));
        }


        private IEnumerator AuthCoroutine (string ip, int id) {
            if (socket != null) {
                socket.Close();
                Debug.Log("TRYING TO OPEN SECOND CONNECTION");
            }
            socket = new WebSocket(new Uri("ws://localhost:8080/events/"));
            yield return StartCoroutine(socket.Connect());

            StartCoroutine(SendPing());

            bb.Clear();
            bb.WriteByte(ClientAPI.Auth);
            bb.WriteInt32(id);
            socket.Send(bb);
        }


        private IEnumerator SendPing () {
            while (socket != null) {
                yield return new WaitForSecondsRealtime(60);
                socket.Send(new byte[0]);
            }
        }


        public void SendHubId (int id) {
            bb.Clear();
            bb.WriteByte(ClientAPI.ToHub);
            bb.WriteByte((byte) id);
            socket.Send(bb);
        }


        public void SendTurnData (TurnData td) {}


        public void SendEndTurn (bool alive) {
            bb.Clear();
            bb.WriteByte(ClientAPI.EndTurn);
            bb.WriteByte((byte) (alive ? 1 : 0));
            socket.Send(bb);
        }

    }

}
