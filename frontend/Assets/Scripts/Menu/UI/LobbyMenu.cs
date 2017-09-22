using Commands.Client;
using Commands.Server;
using Core.UI;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class LobbyMenu : Panel {

        [SerializeField] private Button _cancelButton;

        private WSConnection _connection;
        private MenuScene _menuScene;

        [SerializeField] private Text _text;


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _menuScene = The<MenuScene>.Get();

            CommandExecutor<UpdateLobbyStatusCmd>.AddHandler(OnHubStatusChanged);
            CommandExecutor<RemovedFromLobbyCmd>.AddHandler(OnRemoval);
            _cancelButton.onClick.AddListener(OnClickedCancel);
        }


        private void OnHubStatusChanged (UpdateLobbyStatusCmd obj) {
            UpdateHubStatus(obj.HubId, obj.Players);
        }


        protected override void Deactivate () {
            CommandExecutor<UpdateLobbyStatusCmd>.RemoveHandler(OnHubStatusChanged);
            _cancelButton.onClick.RemoveListener(OnClickedCancel);
        }


        private void OnClickedCancel () {
            new QuitLobbyCmd().Send();
            _text.text = "Выход из комнаты...";
        }


        public void JoinHub (byte hub) {
            _text.text = "Вход в комнату " + hub + "...";
            new JoinLobbyCmd(hub).Send();
        }


        public void UpdateHubStatus (int hub, int players) {
            _text.text = "Игроков в комнате\n" + players + " / " + hub;
        }


        public void OnRemoval (RemovedFromLobbyCmd removedFromLobbyCmd) {
            _menuScene.ShowMainMenu();
            _text.text = "Игра отменена";
        }

    }

}
