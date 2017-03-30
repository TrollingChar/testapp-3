using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;

public class ConnectionMenu : MonoBehaviour {
    public UnityEvent_string_int OnSend;
    public InputField ip, id;

    public void Send () {
        OnSend.Invoke(ip.text, int.Parse(id.text));
    }
}
