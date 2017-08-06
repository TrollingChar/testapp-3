using System;
using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Zenject;


namespace UI {

    public class ConnectionMenu : MonoBehaviour {

        [Inject] private WSConnection _connection;
        [Inject] private PlayerInfoReceivedMessenger _onPlayerInfoReceived;
        
        [SerializeField] private InputField _ipText, _idText;
        [SerializeField] private Button _connectButton;

        private PanelController _panelController;


        private void Awake () {
            _panelController = GetComponent<PanelController>();
            _onPlayerInfoReceived.Subscribe(OnPlayerInfo);
            _connectButton.onClick.AddListener(Send);
        }


        private void OnDestroy () {
            _onPlayerInfoReceived.Unsubscribe(OnPlayerInfo);
            _connectButton.onClick.RemoveListener(Send);
        }

        
        private void Send () {
            _connection.Authorize(_ipText.text, int.Parse(_idText.text));
        }


        private void OnPlayerInfo (PlayerInfo playerInfo) {
            _panelController.Hide();
        }

    }

}
