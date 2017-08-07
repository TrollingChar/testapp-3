using Messengers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace UI.Panels {

    public class MainMenu : Panel {

        [Inject] private PlayerInfoReceivedMessenger _onPlayerInfoReceived;
        [Inject] private GameModeMenu _gameModeMenu;

        [SerializeField] private Button _playButton, _donateButton;


        protected override void Activate () {
            _onPlayerInfoReceived.Subscribe(OnPlayerInfo);
            _playButton.onClick.AddListener(OnClickedPlay);
            _donateButton.onClick.AddListener(OnClickedDonate);
        }


        protected override void Deactivate () {
            _onPlayerInfoReceived.Unsubscribe(OnPlayerInfo);
            _playButton.onClick.RemoveListener(OnClickedPlay);
            _donateButton.onClick.RemoveListener(OnClickedDonate);
        }


        private void OnClickedPlay () {
            Hide();
            _gameModeMenu.Show();
        }


        private void OnClickedDonate () {
            // todo: show donate menu
        }


        private void OnPlayerInfo (PlayerInfo playerInfo) {
            Show();
        }

    }

}
