using Core.UI;
using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Menu.UI {

    public class OpponentSearchMenu : Panel {

        [SerializeField] private Button _cancelButton;

        private WSConnection _connection;
        private MenuScene _menuScene;

        [SerializeField] private Text _text;


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _menuScene = The<MenuScene>.Get();

            _connection.OnHubChanged.Subscribe(UpdateHubStatus);
            _cancelButton.onClick.AddListener(OnClickedCancel);
        }


        protected override void Deactivate () {
            _connection.OnHubChanged.Unsubscribe(UpdateHubStatus);
            _cancelButton.onClick.RemoveListener(OnClickedCancel);
        }


        private void OnClickedCancel () {
            _connection.SendHubId(0);
            _text.text = "Выход из комнаты...";
        }


        public void JoinHub (int hub) {
            _text.text = "Вход в комнату " + hub + "...";
            _connection.SendHubId(hub);
        }


        public void UpdateHubStatus (int hub, int players) {
            if (hub != 0) {
                _text.text = "Игроков в комнате\n" + players + " / " + hub;
                return;
            }

            _menuScene.ShowMainMenu();
            _text.text = "Игра отменена";
        }

    }

}
