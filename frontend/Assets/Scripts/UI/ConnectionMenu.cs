using UnityEngine;
using UnityEngine.UI;
using Utils;


namespace UI {

    public class ConnectionMenu : MonoBehaviour {

        [SerializeField]
        private UnityEvent_string_int onSend;

        [SerializeField]
        private InputField ipText, idText;


        public void Send () {
            onSend.Invoke(ipText.text, int.Parse(idText.text));
        }

    }

}
