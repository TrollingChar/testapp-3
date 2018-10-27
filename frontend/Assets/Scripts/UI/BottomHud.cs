using Core;
using UnityEngine;
using UnityEngine.UI;
using Time = Core.Time;


namespace UI {

    public class BottomHud : Panel {

        private string _gameTime = "";
        private string _turnTime = "";

        [SerializeField] private Text _middleText;
        [SerializeField] private Text _time;
        [SerializeField] private Text _wind;


        private void Awake () {
            The.Battle.OnGameStarted += OnGameStarted;
        }


        private void OnGameStarted () {
            The.Battle.OnGameStarted            -= OnGameStarted;
            The.Battle.TurnTimer.OnChanged      += UpdateTime;
            The.Battle.TurnTimer.OnPauseChanged += OnPauseChanged;
            The.World.OnWindChange              += UpdateWind;
        }


        private void OnPauseChanged (bool value) {
            _time.color = value ? new Color (0.7f, 0.7f, 0.7f) : Color.white;
        }


        private void UpdateTime (Time t) {
            string turnTime = t.ToString ();
            if (_turnTime == turnTime) return;
            _turnTime = turnTime;
            UpdateTimer ();
        }


        public void SetGameTime (string gameTime) {
            if (_gameTime == gameTime) return;
            _gameTime = gameTime;
            UpdateTimer ();
        }


        private void UpdateTimer () {
            _time.text = _turnTime;
//                _turnTime == ""
//                    ? _gameTime == ""
//                        ? ""
//                        : "<size=90>" + _gameTime + "</size>"
//                    : _gameTime == ""
//                        ? "<size=120>" + _turnTime + "</size>"
//                        : "<size=120>" + _turnTime + "</size>\n<size=60>" + _gameTime + "</size>";
        }


        private void UpdateWind (float wind) {
            if      (wind < 0) _wind.text = string.Format ("<- {0:F1}", -wind);
            else if (wind > 0) _wind.text = string.Format ("{0:F1} ->",  wind);
            else               _wind.text = "0.0";
        }

    }

}