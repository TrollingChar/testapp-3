using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class MainMenu : Panel {

        [SerializeField] private Button _playButton, _donateButton;

        private MenuScene MenuScene { get; set; }


        protected override void Activate () {
            MenuScene = The<MenuScene>.Get();
            
            _playButton.onClick.AddListener(OnClickedPlay);
            _donateButton.onClick.AddListener(OnClickedDonate);
        }


        protected override void Deactivate () {
            _playButton.onClick.RemoveListener(OnClickedPlay);
            _donateButton.onClick.RemoveListener(OnClickedDonate);
        }


        private void OnClickedPlay () {
            MenuScene.ShowGameModeMenu();
        }


        private void OnClickedDonate () {
            MenuScene.ShowDonateMenu();
        }

    }

}
