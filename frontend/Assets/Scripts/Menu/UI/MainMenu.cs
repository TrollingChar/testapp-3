using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Menu.UI {

    public class MainMenu : Panel {

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _helpButton;
        [SerializeField] private Button _donateButton;

        private MenuScene MenuScene { get; set; }


        protected override void Activate () {
            MenuScene = The.MenuScene;

            _playButton.onClick.AddListener(OnClickedPlay);
            _helpButton.onClick.AddListener(OnClickedHelp);
            _donateButton.onClick.AddListener(OnClickedDonate);
        }


        protected override void Deactivate () {
            _playButton.onClick.RemoveListener(OnClickedPlay);
            _helpButton.onClick.RemoveListener(OnClickedHelp);
            _donateButton.onClick.RemoveListener(OnClickedDonate);
        }


        private void OnClickedPlay () {
            MenuScene.ShowGameModeMenu();
        }


        private void OnClickedHelp () {
            MenuScene.ShowHelpMenu();
        }


        private void OnClickedDonate () {
            MenuScene.ShowDonateMenu();
//            _donateButton.GetComponentInChildren<Text>().text = "эта кнопка не работает, нажми играть";
        }

    }

}
