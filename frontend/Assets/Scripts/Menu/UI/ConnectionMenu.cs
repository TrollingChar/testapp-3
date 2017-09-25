using Commands.Client;
using Core.UI;
using DataTransfer.Client;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button _connectButton;
        [SerializeField] private InputField _ipText;
        [SerializeField] private InputField _idText;

        private Connection _connection;


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
