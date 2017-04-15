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

    public UnityEvent OnAccountData;
    public UnityEvent_int OnChangeHub;
    public UnityEvent OnStartGame;
    public UnityEvent_int OnPlayerQuit;
    public UnityEvent OnEndGame;
    public UnityEvent OnTurnData;

    ByteBuffer bb = new ByteBuffer();

	// Use this for initialization
    void Start () { }
	
	// Update is called once per frame
    void Update () {
        if (socket == null) return;
        for (byte[] bytes = socket.Recv();
            bytes != null;
            bytes = socket.Recv())
        {
            Debug.Log(BitConverter.ToString(bytes));
            Parse(bytes);
        }
    }

    void Parse(byte[] bytes) {
        MemoryStream stream = new MemoryStream(bytes);
        EndianBinaryReader reader = new EndianBinaryReader(EndianBitConverter.Big, stream);
        switch (reader.ReadByte()) {
            case ServerAPI.ACCOUNT_DATA:
                OnAccountData.Invoke();
                break;
            case ServerAPI.HUB_CHANGED:
                OnChangeHub.Invoke(reader.Read());
                break;
            case ServerAPI.START_GAME:
                OnStartGame.Invoke();
                break;
            case ServerAPI.LEFT_GAME:
                OnPlayerQuit.Invoke(reader.ReadInt32());
                break;
            case ServerAPI.END_GAME:
                OnEndGame.Invoke();
                break;
            case ServerAPI.TURN_DATA:
                OnTurnData.Invoke();
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
        socket = new WebSocket(new Uri("ws://localhost:8080/events/"));
        yield return StartCoroutine(socket.Connect());
        
        bb.Clear();
        bb.WriteByte(ClientAPI.AUTH);
        bb.WriteInt32(id);
        socket.Send(bb);
    }

    public void SendHubId (int id) {
        bb.Clear();
        bb.WriteByte(ClientAPI.TO_HUB);
        bb.WriteByte((byte)id);
        socket.Send(bb);
    }

    public void SendTurnData () {
    }
}
