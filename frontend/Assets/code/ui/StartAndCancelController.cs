using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAndCancelController : MonoBehaviour {

    public PanelController gameModeMenu, opponentSearchMenu;
    public Text opponentSearchStatus;

	// Use this for initialization
    void Start () { }
	
	// Update is called once per frame
    void Update () { }

    public void OnButtonClicked (int hub) {
        opponentSearchStatus.text = "Отправка запроса в комнату " + hub.ToString();
    }

    public void OnHubChanged (int hub) {
        if (hub == 0) {
            gameModeMenu.Show();
            opponentSearchMenu.Hide();
            opponentSearchStatus.text = "Игра отменена";
            return;
        }
        opponentSearchStatus.text = "Игроков в комнате\n0 / " + hub.ToString();
    }
}
