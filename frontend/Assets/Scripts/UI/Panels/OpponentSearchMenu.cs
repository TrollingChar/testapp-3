using Messengers;
using Net;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace UI.Panels {

    public class OpponentSearchMenu : Panel {

        private WSConnection _connection;
        private HubChangedMessenger _onHubChanged;
        private MainMenu _mainMenu;

        [SerializeField] private Text _text;
        [SerializeField] private Button _cancelButton;


        protected override void OnAwake () {
            The<OpponentSearchMenu>.Set(this);
        }


        protected override void Activate () {
            _connection = The<WSConnection>.Get();
            _onHubChanged = _connection.OnHubChanged;
            _mainMenu = The<MainMenu>.Get();
            
            _onHubChanged.Subscribe(UpdateHubStatus);
            _cancelButton.onClick.AddListener(OnClickedCancel);
        }


        protected override void Deactivate () {
            _onHubChanged.Unsubscribe(UpdateHubStatus);
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
