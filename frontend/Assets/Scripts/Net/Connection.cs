using System;
using System.Collections;
using System.IO;
using DataTransfer;
using DataTransfer.Client;
using DataTransfer.Server;
using Net.Utils;
using UnityEngine;
using Utils.Singleton;


namespace Net {

    public class Connection : MonoBehaviour {

        private WebSocket _socket;
        private int _turnDataRead;


        private void Awake () {
            The<Connection>.Set(this);
        }


        public void Work () {}


        public void Update () {
            if (_socket == null) return;

            for (_turnDataRead = 0; _turnDataRead < 2;) {
                var bytes = _socket.Recv();
                if (bytes == null) break;
                Parse(bytes);
            }
        }


        private void OnDisable () {
            if (_socket == null) return;
            _socket.Close();
            _socket = null;
        }


        private void Parse (byte[] bytes) {
            Debug.Log(BitConverter.ToString(bytes));

            using (var stream = new MemoryStream(bytes))
            using (var reader = new BigEndianBinaryReader(stream)) {
                var cmd = (ServerCommand) DTO.Read(reader);
                cmd.Execute();
            }
        }


        public void Send (ClientCommand cmd) {
            var authorizeCommand = cmd as AuthRequestCmd;
            if (authorizeCommand != null) {
                StartCoroutine(AuthCoroutine(authorizeCommand));
                return;
            }
            DoSend(cmd);
        }


        private IEnumerator AuthCoroutine (AuthRequestCmd cmd) {
            if (_socket != null) {
                _socket.Close();
                Debug.Log("TRYING TO OPEN SECOND CONNECTION");
            }
            _socket = new WebSocket(new Uri("ws://localhost:8080/websocket"));
//            _socket = new WebSocket(new Uri("ws://localhost:8080/events/"));
            yield return StartCoroutine(_socket.Connect());

            StartCoroutine(SendPing());
            DoSend(cmd);
        }


        private void DoSend (ClientCommand cmd) {
            using (var stream = new MemoryStream())
            using (var writer = new BigEndianBinaryWriter(stream)) {
                cmd.Write(writer);
                _socket.Send(stream.ToArray());
            }
        }


        private IEnumerator SendPing () {
            while (_socket != null) {
                yield return new WaitForSecondsRealtime(60);
                _socket.Send(new byte[0]);
            }
        }

    }

}
