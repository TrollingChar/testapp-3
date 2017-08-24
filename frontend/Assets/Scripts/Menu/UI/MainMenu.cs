using Core;
using Core.UI;
using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class MainMenu : Panel {

        private GameModeMenu _gameModeMenu;

        private PlayerInfoReceivedMessenger _onPlayerInfoReceived;

        [SerializeField] private Button _playButton, _donateButton;


        protected override void OnAwake () {
            The<MainMenu>.Set(this);
        }


        protected override void Activate () {
            _onPlayerInfoReceived = The<WSConnection>.Get().OnPlayerInfo;
            _gameModeMenu = The<GameModeMenu>.Get();

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
