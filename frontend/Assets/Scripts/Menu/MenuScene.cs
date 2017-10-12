using DataTransfer.Server;
using Menu.UI;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Menu {

    public class MenuScene : MonoBehaviour {

        [SerializeField] private ConnectionMenu _connectionMenu;
        [SerializeField] private GameModeMenu _gameModeMenu;
        [SerializeField] private LobbyMenu _lobbyMenu;
        [SerializeField] private MainMenu _mainMenu;


        private void Awake () {
            The<MenuScene>.Set(this);
            AuthSuccessCmd.OnReceived.Subscribe(OnAuthSuccess);
        }


        private void OnAuthSuccess (AuthSuccessCmd obj) {
            AuthSuccessCmd.OnReceived.Unsubscribe(OnAuthSuccess);
            _connectionMenu.Hide();
            _mainMenu.Show();
        }


        private void OnDestroy () {
            The<MenuScene>.Set(null);
        }


        public void ShowGameModeMenu () {
            _mainMenu.Hide();
            _gameModeMenu.Show();
        }


        public void ShowDonateMenu () {
            Debug.LogError("DonateMenu not implemented");
        }


        public void ShowLobbyMenu (byte hubId) {
            _gameModeMenu.Hide();
            _lobbyMenu.Show();
            _lobbyMenu.JoinHub(hubId);
        }


        public void ShowMainMenu () {
            // todo: hide current menu
            _connectionMenu.Hide();
            _gameModeMenu.Hide();
            _lobbyMenu.Hide();

            _mainMenu.Show();
        }

    }

}
