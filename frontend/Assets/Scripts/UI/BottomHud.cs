using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomHud : MonoBehaviour {
    public Text middleText;
    public Text time;
    //public Button weapon;

    string turnTime = "", gameTime = "";

    public void SetTurnTime (string turnTime) {
        if (this.turnTime == turnTime) return;
        this.turnTime = turnTime;
        UpdateTimer();
    }

    public void SetGameTime (string gameTime) {
        if (this.gameTime == gameTime) return;
        this.gameTime = gameTime;
        UpdateTimer();
    }

    void UpdateTimer () {
        time.text =
            turnTime == ""
            ? gameTime == ""
                ? ""
                : "<size=90>" + gameTime + "</size>"
            : gameTime == ""
                ? "<size=120>" + turnTime + "</size>"
                : "<size=120>" + turnTime + "</size>\n<size=60>" + gameTime + "</size>";
    }
}
