using Battle;
using Commands.Server;
using Core;
using Menu.UI;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Menu {

    public class MenuScene : MonoBehaviour {

        [SerializeField] private ConnectionMenu _connectionMenu;
        [SerializeField] private MainMenu _mainMenu;
        [SerializeField] private GameModeMenu _gameModeMenu;
        [SerializeField] private OpponentSearchMenu _opponentSearchMenu;

        private WSConnection Connection { get; set; }


        private void Awake () {
            The<MenuScene>.Set(this);
            Connection = The<WSConnection>.Get();
            CommandExecutor<AuthorizedCommand>.AddHandler(OnAuthorized);
//            CommandExecutor<GameStartedCommand>.AddHandler(OnGameStarted);
            
        }

        private void OnAuthorized(AuthorizedCommand obj)
        {
            _connectionMenu.Hide();
            _mainMenu.Show();
        }

//        private void OnGameStarted(GameStartedCommand obj)
//        {
//        }

        private void OnDestroy () {
            CommandExecutor<AuthorizedCommand>.RemoveHandler(OnAuthorized);
//            CommandExecutor<GameStartedCommand>.RemoveHandler(OnGameStarted);
            The<MenuScene>.Set(null);
        }


        public void ShowGameModeMenu () {
            _mainMenu.Hide();
            _gameModeMenu.Show();
        }


        public void ShowDonateMenu () {
            Debug.LogError("DonateMenu not implemented");
        }


        public void ShowOpponentSearchMenu (int hubId) {
            _gameModeMenu.Hide();
            _opponentSearchMenu.Show();
            _opponentSearchMenu.JoinHub(hubId);
        }


        public void ShowMainMenu () {
            // todo: hide current menu
            _connectionMenu.Hide();
            _gameModeMenu.Hide();
            _opponentSearchMenu.Hide();

            _mainMenu.Show();
        }

    }

}
