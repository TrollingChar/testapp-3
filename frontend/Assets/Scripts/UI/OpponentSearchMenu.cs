using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace UI {

    internal class OpponentSearchMenu : MonoBehaviour {

        [SerializeField] private Text _text;
        [SerializeField] private UnityEvent _onCancel;


        public void JoinHub (int hub) {
            _text.text = "Отправка запроса в комнату " + hub;
        }


        public void UpdateHubStatus (int hub, int players) {
            if (hub == 0) {
                _onCancel.Invoke();
                _text.text = "Игра отменена";
                return;
            }
            _text.text = "Игроков в комнате\n" + players + " / " + hub;
        }

    }

}
