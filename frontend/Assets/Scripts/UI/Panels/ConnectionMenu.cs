using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace UI.Panels {

    public class ConnectionMenu : Panel {

        [Inject] private WSConnection _connection;
        [Inject] private PlayerInfoReceivedMessenger _onPlayerInfoReceived;
        
        [SerializeField] private InputField _ipText, _idText;
        [SerializeField] private Button _connectButton;


        protected override void Activate () {
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
