using Core.UI;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button _connectButton;
        [SerializeField] private InputField _ipText, _idText;

        private WSConnection _connection;


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _connectButton.onClick.AddListener(Send);
        }


        protected override void Deactivate () {
            _connectButton.onClick.RemoveListener(Send);
        }


        private void Send () {
            _connection.Authorize(_ipText.text, int.Parse(_idText.text));
        }

    }

}
