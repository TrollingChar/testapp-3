using Core;
using UI;
using UnityEngine;
using UnityEngine.UI;


namespace Menu.UI {

    public class GameModeMenu : Panel {

        [SerializeField] private Button _1Player;
        [SerializeField] private Button _2Players;
        [SerializeField] private Button _3Players;
        [SerializeField] private Button _4Players;
        [SerializeField] private Button _backButton;

        private MenuScene MenuScene { get; set; }


        protected override void Activate () {
            MenuScene = The.MenuScene;

            _1Player.onClick.AddListener(OnClicked1Player);
            _2Players.onClick.AddListener(OnClicked2Players);
            _3Players.onClick.AddListener(OnClicked3Players);
            _4Players.onClick.AddListener(OnClicked4Players);
            _backButton.onClick.AddListener(OnClickedBack);
        }


        protected override void Deactivate () {
            _1Player.onClick.RemoveListener(OnClicked1Player);
            _2Players.onClick.RemoveListener(OnClicked2Players);
            _3Players.onClick.RemoveListener(OnClicked3Players);
            _4Players.onClick.RemoveListener(OnClicked4Players);
            _backButton.onClick.RemoveListener(OnClickedBack);
        }


        private void OnClicked1Player  () { MenuScene.ShowLobbyMenu(1); } 
        private void OnClicked2Players () { MenuScene.ShowLobbyMenu(2); } 
        private void OnClicked3Players () { MenuScene.ShowLobbyMenu(3); } 
        private void OnClicked4Players () { MenuScene.ShowLobbyMenu(4); }
        
        
        private void OnClickedBack () {
            MenuScene.ShowMainMenu();
        }

    }

}
