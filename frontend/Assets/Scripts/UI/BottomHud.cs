using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class BottomHud : MonoBehaviour {

        [SerializeField] private Text middleText;
        [SerializeField] private Text time;

        private string turnTime = "";
        private string gameTime = "";


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


        private void UpdateTimer () {
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

}
