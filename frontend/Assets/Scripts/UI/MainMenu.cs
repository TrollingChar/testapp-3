using System;
using Messengers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace UI {

    public class MainMenu : MonoBehaviour {

        [Inject] private PlayerInfoReceivedMessenger _onPlayerInfoReceived;

        [SerializeField] private Button _playButton, _donateButton;

        private PanelController _panelController;


        private void Awake () {
            _panelController = GetComponent<PanelController>();
            _onPlayerInfoReceived.Subscribe(OnPlayerInfo);
            _playButton.onClick.AddListener(OnClickedPlay);
            _donateButton.onClick.AddListener(OnClickedDonate);
        }


        private void OnClickedPlay () {
            _panelController.Hide();
        }


        private void OnClickedDonate () {
            // todo: show donate menu
        }


        private void OnDestroy () {
            _onPlayerInfoReceived.Unsubscribe(OnPlayerInfo);
            _playButton.onClick.RemoveListener(OnClickedPlay);
            _donateButton.onClick.RemoveListener(OnClickedDonate);
        }


        private void OnPlayerInfo (PlayerInfo playerInfo) {
            _panelController.Show();
        }

    }

}
