using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Attributes;
using Battle;
using Commands;
using Commands.Client;
using Commands.Server;
using Core;
using Messengers;
using Net.Utils;
using Net.Utils.Conversion;
using Net.Utils.IO;
using UnityEngine;
using Utils.Singleton;


namespace Net {

    public class WSConnection : MonoBehaviour {

        private WebSocket _socket;

        private readonly ByteBuffer _bb = new ByteBuffer();

        private int _turnDataRead;


        private void Awake () {
            The<WSConnection>.Set(this);
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
            var stream = new MemoryStream(bytes);
            var reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

            var cmd = Serialization<IServerCommand>.GetNewInstanceByCode(reader.ReadByte());
            cmd.Deserialize(reader);
            cmd.Execute();
            
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


        public void Send (IClientCommand cmd) {
            var writer = new EndianBinaryWriter(EndianBitConverter.Big, null); // here
            writer.WriteByte(Serialization<IClientCommand>.GetCodeByType(cmd.GetType()));            
            cmd.Serialize(writer);
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
