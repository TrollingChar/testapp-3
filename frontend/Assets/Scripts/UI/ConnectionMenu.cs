using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;

public class ConnectionMenu : MonoBehaviour {
    public UnityEvent_string_int onSend;
    public InputField ip, id;

    public void Send () {
        onSend.Invoke(ip.text, int.Parse(id.text));
    }
}
