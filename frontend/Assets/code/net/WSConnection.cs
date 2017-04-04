using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WSConnection : MonoBehaviour {
    WebSocket socket;

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
        }
    }

    public void Authorize (string s, int id) {
        Debug.Log("kek");
        socket = new WebSocket(new Uri("ws://localhost:8080/events/"));
        StartCoroutine(socket.Connect());
    }
}
