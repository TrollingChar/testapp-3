using Messengers;
using Net;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;


namespace UI.Panels {

    internal class OpponentSearchMenu : Panel {

        [Inject] private WSConnection _connection;
        [Inject] private HubChangedMessenger _hubChangedMessenger;
        [Inject] private MainMenu _mainMenu;

        [SerializeField] private Text _text;
        [SerializeField] private Button _cancelButton;


        protected override void Activate () {
            _hubChangedMessenger.Subscribe(UpdateHubStatus);
            _cancelButton.onClick.AddListener(OnClickedCancel);
        }


        protected override void Deactivate () {
            _hubChangedMessenger.Unsubscribe(UpdateHubStatus);
            _cancelButton.onClick.RemoveListener(OnClickedCancel);
        }


        private void OnClickedCancel () {
            _connection.SendHubId(0);
            _text.text = "Выход из комнаты...";
        }


        public void JoinHub (int hub) {
            Show();
            _text.text = "Вход в комнату " + hub + "...";
            _connection.SendHubId(hub);
        }


        public void UpdateHubStatus (int hub, int players) {
            if (hub != 0) {
                _text.text = "Игроков в комнате\n" + players + " / " + hub;
                return;
            }
            
            Hide();
            _mainMenu.Show();
            _text.text = "Игра отменена";
        }

    }

}
