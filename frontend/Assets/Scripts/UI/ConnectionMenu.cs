using UnityEngine;
using UnityEngine.UI;
using Utils;


namespace UI {

    public class ConnectionMenu : MonoBehaviour {

        [SerializeField] private UnityEvent_string_int _onSend;
        [SerializeField] private InputField
            _ipText,
            _idText;


        public void Send () {
            _onSend.Invoke(_ipText.text, int.Parse(_idText.text));
        }

    }

}
