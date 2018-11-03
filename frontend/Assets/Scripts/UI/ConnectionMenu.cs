using Core;
using DataTransfer.Client;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Random;


namespace UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button     _connectButton;
        [SerializeField] private InputField _idText;
        [SerializeField] private InputField _serverText;

        private Connection _connection;


        protected override void Activate () {
            _connection = The.Connection;
            _connectButton.onClick.AddListener (Send);
            _idText.text = RNG.Int (10000).ToString ();
            _serverText.text = "wss://worms-3.herokuapp.com/websocket";
        }


        protected override void Deactivate () {
            _connectButton.onClick.RemoveListener (Send);
        }


        private void Send () {
            _connection.Authorize (int.Parse (_idText.text), _serverText.text);
//            _connection.Send (new AuthRequestCmd (int.Parse (_idText.text)));
        }

    }

}