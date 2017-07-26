﻿using System;
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

        [SerializeField] private UnityEvent_int _onAccountData;
        [SerializeField] private UnityEvent_int_int _onChangeHub;
        [SerializeField] private UnityEvent_GameData _onStartGame;
        [SerializeField] private UnityEvent_int _onPlayerQuit;
        [SerializeField] private UnityEvent_int _onPlayerWin;
        [SerializeField] private UnityEvent_TurnData _onTurnData;
        [SerializeField] private UnityEvent _onNoWinner;
        [SerializeField] private UnityEvent_int _onNewTurn;

        private WebSocket _socket;
        private ByteBuffer _bb = new ByteBuffer();

        private int _turnDataRead;


        public void Work () {
            if (_socket == null) return;

            _turnDataRead = 0;
            for (
                byte[] bytes = _socket.Recv();
                bytes != null && _turnDataRead < 2;
                bytes = _socket.Recv()
            ) {
                Debug.Log(BitConverter.ToString(bytes));
                Parse(bytes);
            }
        }


        private void OnDisable () {
            if (_socket == null) return;
            _socket.Close();
            _socket = null;
        }


        private void Parse (byte[] bytes) {
            var stream = new MemoryStream(bytes);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

            switch (reader.ReadByte()) {
                case ServerAPI.AccountData:
                    _onAccountData.Invoke(reader.ReadInt32());
                    break;

                case ServerAPI.HubChanged:
                    _onChangeHub.Invoke(reader.ReadByte(), reader.ReadByte());
                    break;

                case ServerAPI.StartGame:
                    int seed = reader.ReadInt32();
                    var players = new List<int>();
                    for (byte i = 0, end = reader.ReadByte(); i < end; ++i) players.Add(reader.ReadInt32());
                    _onStartGame.Invoke(new GameData(seed, players));
                    break;

                case ServerAPI.LeftGame:
                    _onPlayerQuit.Invoke(reader.ReadInt32());
                    break;

                case ServerAPI.ShowWinner:
                    _onPlayerWin.Invoke(reader.ReadInt32());
                    break;

                case ServerAPI.TurnData:
                    ++_turnDataRead;
                    var td = new TurnData(reader.ReadByte(), reader.ReadSingle(), reader.ReadSingle());
                    _onTurnData.Invoke(td);
                    break;

                case ServerAPI.NoWinner:
                    _onNoWinner.Invoke();
                    break;

                case ServerAPI.NewTurn:
                    _onNewTurn.Invoke(reader.ReadInt32());
                    break;
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


        public void SendTurnData (TurnData td) {}


        public void SendEndTurn (bool alive) {
            _bb.Clear();
            _bb.WriteByte(ClientAPI.EndTurn);
            _bb.WriteByte((byte) (alive ? 1 : 0));
            _socket.Send(_bb);
        }

    }

}