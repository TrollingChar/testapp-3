using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAndCancelController : MonoBehaviour {

    public PanelController gameModeMenu, opponentSearchMenu;
    public Text opponentSearchStatus;

    public void OnButtonClicked (int hub) {
        opponentSearchStatus.text = "Отправка запроса в комнату " + hub.ToString();
    }

    public void OnHubChanged (int hub, int players) {
        if (hub == 0) {
            gameModeMenu.Show();
            opponentSearchMenu.Hide();
            opponentSearchStatus.text = "Игра отменена";
            return;
        }
        opponentSearchStatus.text = "Игроков в комнате\n" + players.ToString() + " / " + hub.ToString();
    }
}
