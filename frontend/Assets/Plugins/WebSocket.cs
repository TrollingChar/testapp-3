using System;
using System.Text;
using System.Collections;
#if UNITY_WEBGL && !UNITY_EDITOR
using System.Runtime.InteropServices;
#else
using System.Collections.Generic;
#endif

public class WebSocket {

    private Uri mUrl;


    public WebSocket (Uri url) {
        mUrl = url;

        string protocol = mUrl.Scheme;
        if (!protocol.Equals("ws") && !protocol.Equals("wss"))
            throw new ArgumentException("Unsupported protocol: " + protocol);
    }


    public void SendString (string str) {
        Send(Encoding.UTF8.GetBytes(str));
    }


    public string RecvString () {
        var retval = Recv();
        if (retval == null) return null;
        return Encoding.UTF8.GetString(retval);
    }


#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern int SocketCreate (string url);


    [DllImport("__Internal")]
    private static extern int SocketState (int socketInstance);


    [DllImport("__Internal")]
    private static extern void SocketSend (int socketInstance, byte[] ptr, int length);


    [DllImport("__Internal")]
    private static extern void SocketRecv (int socketInstance, byte[] ptr, int length);


    [DllImport("__Internal")]
    private static extern int SocketRecvLength (int socketInstance);


    [DllImport("__Internal")]
    private static extern void SocketClose (int socketInstance);


    [DllImport("__Internal")]
    private static extern int SocketError (int socketInstance, byte[] ptr, int length);


    int m_NativeRef;


    public void Send (byte[] buffer) {
        SocketSend(m_NativeRef, buffer, buffer.Length);
    }


    public byte[] Recv () {
        int length = SocketRecvLength(m_NativeRef);
        if (length == 0) return null;
        var buffer = new byte[length];
        SocketRecv(m_NativeRef, buffer, length);
        return buffer;
    }


    public IEnumerator Connect () {
        m_NativeRef = SocketCreate(mUrl.ToString());

        while (SocketState(m_NativeRef) == 0) yield return 0;
    }


    public void Close () {
        SocketClose(m_NativeRef);
    }


    public string error {
        get {
            const int bufsize = 1024;
            var buffer = new byte[bufsize];
            int result = SocketError(m_NativeRef, buffer, bufsize);

            return result == 0 ? null : Encoding.UTF8.GetString(buffer);
        }
    }
#else
    private WebSocketSharp.WebSocket m_Socket;
    private readonly Queue<byte[]> m_Messages = new Queue<byte[]>();
    private bool m_IsConnected;
    private string m_Error;


    public IEnumerator Connect () {
        m_Socket = new WebSocketSharp.WebSocket(mUrl.ToString());
        m_Socket.OnMessage += (sender, e) => m_Messages.Enqueue(e.RawData);
        m_Socket.OnOpen += (sender, e) => m_IsConnected = true;
        m_Socket.OnError += (sender, e) => m_Error = e.Message;
        m_Socket.ConnectAsync();
        while (!m_IsConnected && m_Error == null) yield return 0;
    }


    public void Send (byte[] buffer) {
        m_Socket.Send(buffer);
    }


    public byte[] Recv () {
        return m_Messages.Count == 0 ? null : m_Messages.Dequeue();
    }


    public void Close () {
        m_Socket.Close();
    }


    public string error {
        get { return m_Error; }
    }
#endif

}
