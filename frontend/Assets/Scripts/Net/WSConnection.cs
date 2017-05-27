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
    public UnityEvent_TurnData onTurnData;

    int turnDataRead;

    public void Work () {
        if (socket == null) return;

        turnDataRead = 0;
        for (byte[] bytes = socket.Recv();
            bytes != null && turnDataRead < 2;
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
        var stream = new MemoryStream(bytes);
        var reader = new EndianBinaryReader(EndianBitConverter.Big, stream);
        switch (reader.ReadByte()) {
            case ServerAPI.AccountData:
                onAccountData.Invoke();
                break;
            case ServerAPI.HubChanged:
                onChangeHub.Invoke(reader.ReadByte(), reader.ReadByte());
                break;
            case ServerAPI.StartGame:
                onStartGame.Invoke(reader.ReadInt32());
                for (byte i = 0, end = reader.ReadByte(); i < end; ++i) reader.ReadInt32();
                break;
            case ServerAPI.LeftGame:
                onPlayerQuit.Invoke(reader.ReadInt32());
                break;
            case ServerAPI.EndGame:
                onEndGame.Invoke();
                break;
            case ServerAPI.TurnData:
                ++turnDataRead;
                W3.TurnData td = new W3.TurnData(reader.ReadByte(), reader.ReadSingle(), reader.ReadSingle());
                onTurnData.Invoke(td);
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

    public void SendTurnData (W3.TurnData td) {
    }

    public void SendEndTurn (bool alive) {
        bb.Clear();
        bb.WriteByte(ClientAPI.EndTurn);
        bb.WriteByte((byte)(alive ? 0 : 1));
        socket.Send(bb);
    }
}
