using Core.UI;
using DataTransfer.Client;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button _connectButton;
        private Connection _connection;
        [SerializeField] private InputField _idText;
        [SerializeField] private InputField _ipText;


        protected override void Activate () {
            _connection = The<Connection>.Get();
            _connectButton.onClick.AddListener(Send);
        }


        protected override void Deactivate () {
            _connectButton.onClick.RemoveListener(Send);
        }


        private void Send () {
            _connection.Send(new AuthRequestCmd(_idText.text, int.Parse(_idText.text)));
        }

    }

}
