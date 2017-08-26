using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class GameModeMenu : Panel {

        [SerializeField] private Button _1Player, _2Players, _3Players, _backButton;

        private MenuScene MenuScene { get; set; }


        protected override void Activate () {
            MenuScene = The<MenuScene>.Get();
            
            _1Player.onClick.AddListener(OnClicked1Player);
            _2Players.onClick.AddListener(OnClicked2Players);
            _3Players.onClick.AddListener(OnClicked3Players);
            _backButton.onClick.AddListener(OnClickedBack);
        }


        protected override void Deactivate () {
            _1Player.onClick.RemoveListener(OnClicked1Player);
            _2Players.onClick.RemoveListener(OnClicked2Players);
            _3Players.onClick.RemoveListener(OnClicked3Players);
            _backButton.onClick.RemoveListener(OnClickedBack);
        }


        private void OnClicked1Player () {
            MenuScene.ShowOpponentSearchMenu(1);
        }


        private void OnClicked2Players () {
            MenuScene.ShowOpponentSearchMenu(2);
        }


        private void OnClicked3Players () {
            MenuScene.ShowOpponentSearchMenu(3);
        }


        private void OnClickedBack () {
            MenuScene.ShowMainMenu();
        }

    }

}
