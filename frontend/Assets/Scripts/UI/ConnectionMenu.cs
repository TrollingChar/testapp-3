using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;

public class ConnectionMenu : MonoBehaviour {
    [SerializeField]
    UnityEvent_string_int onSend;

    [SerializeField]
    InputField ipText, idText;

    public void Send () {
        onSend.Invoke(ipText.text, int.Parse(idText.text));
    }
}
