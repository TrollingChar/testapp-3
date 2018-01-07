using Core;
using Core.UI;
using DataTransfer.Client;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;


namespace Menu.UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button _connectButton;
        private Connection _connection;
        [SerializeField] private InputField _idText;


        protected override void Activate () {
            _connection = The.Connection;
            _connectButton.onClick.AddListener(Send);
            _idText.text = RNG.Int(10000).ToString();
        }


        protected override void Deactivate () {
            _connectButton.onClick.RemoveListener(Send);
        }


        private void Send () {
            _connection.Send(new AuthRequestCmd(int.Parse(_idText.text)));
        }

    }

}
