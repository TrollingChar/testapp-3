using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace UI.Panels {

    public class ConnectionMenu : Panel {

        private WSConnection _connection;
        private PlayerInfoReceivedMessenger _onPlayerInfoReceived;
        
        [SerializeField] private InputField _ipText, _idText;
        [SerializeField] private Button _connectButton;


        protected override void OnAwake () {
            The<ConnectionMenu>.Set(this);
        }


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _onPlayerInfoReceived = _connection.OnPlayerInfoReceived;
            
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
