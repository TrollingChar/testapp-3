using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

class OpponentSearchMenu : MonoBehaviour {
    [SerializeField] Text text;
    [SerializeField] UnityEvent onCancel;

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