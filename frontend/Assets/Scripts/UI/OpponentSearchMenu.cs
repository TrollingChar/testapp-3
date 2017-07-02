using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


internal class OpponentSearchMenu : MonoBehaviour {

    [SerializeField]
    private Text text;

    [SerializeField]
    private UnityEvent onCancel;


    public void JoinHub (int hub) {
        text.text = "Отправка запроса в комнату " + hub;
    }


    public void UpdateHubStatus (int hub, int players) {
        if (hub == 0) {
            onCancel.Invoke();
            text.text = "Игра отменена";
            return;
        }
        text.text = "Игроков в комнате\n" + players + " / " + hub;
    }

}
