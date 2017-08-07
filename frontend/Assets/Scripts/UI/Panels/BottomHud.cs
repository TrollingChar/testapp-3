using UnityEngine;
using UnityEngine.UI;


namespace UI.Panels {

    public class BottomHud : MonoBehaviour {

        [SerializeField] private Text _middleText;
        [SerializeField] private Text _time;

        private string _turnTime = "";
        private string _gameTime = "";


        public void SetTurnTime (string turnTime) {
            if (_turnTime == turnTime) return;
            _turnTime = turnTime;
            UpdateTimer();
        }


        public void SetGameTime (string gameTime) {
            if (_gameTime == gameTime) return;
            _gameTime = gameTime;
            UpdateTimer();
        }


        private void UpdateTimer () {
            _time.text =
                _turnTime == ""
                    ? _gameTime == ""
                        ? ""
                        : "<size=90>" + _gameTime + "</size>"
                    : _gameTime == ""
                        ? "<size=120>" + _turnTime + "</size>"
                        : "<size=120>" + _turnTime + "</size>\n<size=60>" + _gameTime + "</size>";
        }

    }

}
