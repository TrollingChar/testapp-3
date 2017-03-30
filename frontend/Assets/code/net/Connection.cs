using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Net.Sockets;
using System.Text;
using UnityEngine.UI;
using System;
using System.IO;
using MiscUtil.IO;
using MiscUtil.Conversion;

public class Connection : MonoBehaviour {
    public UnityEvent OnConfirmAuth;
    public UnityEvent OnStartGame;
    public UnityEvent OnConfirmCancel;
    public UnityEvent_int OnStartTurn;
    public UnityEvent OnTurnData;
    public UnityEvent OnEndGame;
    public UnityEvent_int OnPlayerLeft;
    public UnityEvent_string OnAnyData;
    public Queue<byte[]> queue = new Queue<byte[]>();
    TcpClient client;
    NetworkStream stream;
    EndianBinaryReader reader;
    //BinaryWriter writer;
    ByteBuffer bb = new ByteBuffer();

	// Use this for initialization
    void Start () { }

    public void Authorize (string ip, int id) {
        if (stream == null) {
            client = new TcpClient(ip, 7675);
            stream = client.GetStream();
            reader = new EndianBinaryReader(EndianBitConverter.Big, stream);
            //writer = new BinaryWriter(stream);
        }
        SendId(id);
    }
	
	// Update is called once per frame
    void Update () {
        if (stream == null) return;
        
        if (stream.DataAvailable) {
            int length = stream.ReadByte() << 8 | stream.ReadByte();
            byte[] bytes = reader.ReadBytes(length);
            queue.Enqueue(bytes);
        }
        
        while (queue.Count > 0) {
            byte[] bytes = queue.Dequeue();
            Parse(bytes);
            OnAnyData.Invoke("not implemented");
        }
        
	}

    void Parse (byte[] bytes) {
        MemoryStream stream = new MemoryStream(bytes);
        EndianBinaryReader reader = new EndianBinaryReader(EndianBitConverter.Big, stream);

        switch (reader.ReadByte()) {
            case ServerAPI.ACCOUNT_DATA:
                Debug.Log(reader.ReadByte() != 0 ? "Бан\n" : "" +
                    "Снабжение: " + reader.ReadInt32().ToString() +
                    "\nРазведданные: " + reader.ReadInt32().ToString() +
                    "\nОпыт: " + reader.ReadInt32().ToString() +
                    "\nЗвезды: " + reader.ReadInt32().ToString());
                OnConfirmAuth.Invoke();
                break;
            default:
                break;
        }
        reader.Close();
        stream.Close();
    }
    /*
    public void Send (string data) {
        byte[] bytes = Encoding.UTF8.GetBytes(data);
        int length = bytes.Length;
        stream.WriteByte((byte)(length >> 8));
        stream.WriteByte((byte)(length));
        stream.Write(bytes, 0, length);
        stream.Flush();
    }
    */
    public void Send (byte[] bytes, int length) {
        //int length = bytes.Length;
        stream.WriteByte((byte)(length >> 8));
        stream.WriteByte((byte)(length));
        stream.Write(bytes, 0, length);
        stream.Flush();
    }
    /*
    public void Send (InputField input) {
        Send(input.text);
    }
    */
    public void SendId (int id) {
        bb.Clear();
        bb.WriteByte(ClientAPI.AUTH);
        bb.WriteInt32(id);
        Send(bb.bytes, bb.length);
    }

    public void SendStartGame () {
    }

    public void SendCancel () {
    }

    public void SendSync (bool alive) {
    }

    public void SendTurnData () {
    }

    public void SendQuitGame () {
    }
}
