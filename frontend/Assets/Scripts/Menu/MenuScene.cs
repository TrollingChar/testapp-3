using Core;
using DataTransfer.Server;
using UI;
using UnityEngine;


namespace Menu {

    public class MenuScene : MonoBehaviour {

        [SerializeField] private ConnectionMenu _connectionMenu;
        [SerializeField] private GameModeMenu   _gameModeMenu;
        [SerializeField] private LobbyMenu      _lobbyMenu;
        [SerializeField] private MainMenu       _mainMenu;
        [SerializeField] private HelpMenu       _helpMenu;
        [SerializeField] private DonateMenu     _donateMenu;


        private void Awake () {
            The.MenuScene = this;
            bool bypassConnection = (bool) The.SceneSwitcher.Data[0];
            if (bypassConnection) {
                _connectionMenu.Hide (true);
                _mainMenu.Show ();
            }
            else {
                AuthSuccessCmd.OnReceived += OnAuthSuccess;
            }
        }


        private void OnAuthSuccess (AuthSuccessCmd obj) {
            AuthSuccessCmd.OnReceived -= OnAuthSuccess;
            _connectionMenu.Hide ();
            _mainMenu.Show ();
        }


        private void OnDestroy () {
            The.MenuScene = null;
        }


        public void ShowGameModeMenu () {
            _mainMenu.Hide ();
            _gameModeMenu.Show ();
        }


        public void ShowHelpMenu () {
            _mainMenu.Hide ();
            _helpMenu.Show ();
        }


        public void ShowDonateMenu () {
            _mainMenu.Hide ();
            _donateMenu.Show ();
        }


        public void ShowLobbyMenu (byte hubId) {
            _gameModeMenu.Hide ();
            _lobbyMenu.Show ();
            _lobbyMenu.JoinHub (hubId);
        }


        public void ShowMainMenu () {
            _connectionMenu.Hide ();
            _gameModeMenu.Hide ();
            _lobbyMenu.Hide ();
            _helpMenu.Hide ();
            _donateMenu.Hide ();

            _mainMenu.Show ();
        }

    }

}