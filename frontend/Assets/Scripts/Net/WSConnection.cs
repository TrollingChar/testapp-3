using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;

public class WSConnection : MonoBehaviour {
    WebSocket socket;
    ByteBuffer bb = new ByteBuffer();

    public UnityEvent onAccountData;
    public UnityEvent_int_int onChangeHub;
    public UnityEvent_int onStartGame;
    public UnityEvent_int onPlayerQuit;
    public UnityEvent onEndGame;
    public UnityEvent onTurnData;

    public void Work () {
        if (socket == null) return;
        for (byte[] bytes = socket.Recv();
            bytes != null;
            bytes = socket.Recv())
        {
            Debug.Log(BitConverter.ToString(bytes));
            Parse(bytes);
        }
    }

    void OnDisable () {
        if (socket != null) {
            socket.Close();
            socket = null;
        }
    }

    void Parse(byte[] bytes) {
        MemoryStream stream = new MemoryStream(bytes);
        EndianBinaryReader reader = new EndianBinaryReader(EndianBitConverter.Big, stream);
        switch (reader.ReadByte()) {
            case ServerAPI.AccountData:
                onAccountData.Invoke();
                break;
            case ServerAPI.HubChanged:
                onChangeHub.Invoke(reader.Read(), reader.Read());
                break;
            case ServerAPI.StartGame:
                onStartGame.Invoke(reader.Read());
                for (byte i = 0, end = reader.ReadByte(); i < end; ++i) reader.Read();
                break;
            case ServerAPI.LeftGame:
                onPlayerQuit.Invoke(reader.ReadInt32());
                break;
            case ServerAPI.EndGame:
                onEndGame.Invoke();
                break;
            case ServerAPI.TurnData:
                onTurnData.Invoke();
                break;
            default:
                break;
        }
        reader.Close();
        stream.Close();
    }

    public void Authorize (string ip, int id) {
        StartCoroutine(AuthCoroutine(ip, id));
    }

    IEnumerator AuthCoroutine(string ip, int id) {
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

    IEnumerator SendPing () {
        while (socket != null) {
            yield return new WaitForSecondsRealtime(60);
            socket.Send(new byte[0]);
        }
    }

    public void SendHubId (int id) {
        bb.Clear();
        bb.WriteByte(ClientAPI.ToHub);
        bb.WriteByte((byte)id);
        socket.Send(bb);
    }

    public void SendTurnData () {
    }
}
