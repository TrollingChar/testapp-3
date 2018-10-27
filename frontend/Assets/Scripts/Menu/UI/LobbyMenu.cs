using Core;
using DataTransfer.Client;
using DataTransfer.Server;
using Net;
using UI;
using UnityEngine;
using UnityEngine.UI;


namespace Menu.UI {

    public class LobbyMenu : Panel {

        [SerializeField] private Button _cancelButton;
        [SerializeField] private Text   _text;

        private Connection _connection;
        private MenuScene  _menuScene;


        protected override void Activate () {
            _connection = The.Connection;
            _menuScene  = The.MenuScene;

            LobbyStatusCmd.OnReceived += OnLobbyStatusChanged;
            LeftLobbyCmd.OnReceived   += OnLeftLobby;
            _cancelButton.onClick.AddListener (OnClickedCancel);
        }


        private void OnLobbyStatusChanged (LobbyStatusCmd cmd) {
            UpdateHubStatus (cmd.HubId, cmd.Players);
        }


        protected override void Deactivate () {
            LobbyStatusCmd.OnReceived -= OnLobbyStatusChanged;
            LeftLobbyCmd.OnReceived   -= OnLeftLobby;
            _cancelButton.onClick.RemoveListener (OnClickedCancel);
        }


        private void OnClickedCancel () {
            _connection.Send (new LeaveLobbyCmd ());
            _text.text = "Выход из комнаты...";
        }


        public void JoinHub (byte lobbyId) {
            _text.text = "Вход в комнату " + lobbyId + "...";
            _connection.Send (new JoinLobbyCmd (lobbyId));
        }


        public void UpdateHubStatus (int hub, int players) {
            _text.text = "Игроков в комнате\n" + players + " / " + hub;
        }


        public void OnLeftLobby (LeftLobbyCmd cmd) {
            _menuScene.ShowMainMenu ();
            _text.text = "Игра отменена";
        }

    }

}