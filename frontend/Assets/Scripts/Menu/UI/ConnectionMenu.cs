using Core;
using Core.UI;
using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class ConnectionMenu : Panel {

        [SerializeField] private Button _connectButton;

        private WSConnection _connection;

        [SerializeField] private InputField _ipText, _idText;
        private PlayerInfoReceivedMessenger _onPlayerInfoReceived;


        protected override void OnAwake () {
            The<ConnectionMenu>.Set(this);
        }


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _onPlayerInfoReceived = _connection.OnPlayerInfo;

            _onPlayerInfoReceived.Subscribe(OnPlayerInfo);
            _connectButton.onClick.AddListener(Send);
        }


        protected override void Deactivate () {
            _onPlayerInfoReceived.Unsubscribe(OnPlayerInfo);
            _connectButton.onClick.RemoveListener(Send);
        }


        private void Send () {
            _connection.Authorize(_ipText.text, int.Parse(_idText.text));
        }


        private void OnPlayerInfo (PlayerInfo playerInfo) {
            Hide();
        }

    }

}
