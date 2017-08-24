using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class GameModeMenu : Panel {

        [SerializeField] private Button _1Player, _2Players, _3Players, _backButton;

        private MainMenu _mainMenu;
        private OpponentSearchMenu _opponentSearchMenu;


        protected override void OnAwake () {
            The<GameModeMenu>.Set(this);
        }


        protected override void Activate () {
            _mainMenu = The<MainMenu>.Get();
            _opponentSearchMenu = The<OpponentSearchMenu>.Get();

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
            Hide();
            _opponentSearchMenu.JoinHub(1);
        }


        private void OnClicked2Players () {
            Hide();
            _opponentSearchMenu.JoinHub(2);
        }


        private void OnClicked3Players () {
            Hide();
            _opponentSearchMenu.JoinHub(3);
        }


        private void OnClickedBack () {
            Hide();
            _mainMenu.Show();
        }

    }

}
