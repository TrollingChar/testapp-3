using Battle.State;
using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Time = Core.Time;


namespace Battle.UI {

    public class BottomHud : Panel {

        private GameStateController _state;

        private string _gameTime = "";
        private string _turnTime = "";

        [SerializeField] private Text _middleText;
        [SerializeField] private Text _time;
        [SerializeField] private Text _wind;


        private void Awake () {
            The.BattleScene.OnBattleLoaded.Subscribe(OnBattleLoaded);
        }


        private void OnBattleLoaded () {
            The.BattleScene.OnBattleLoaded.Unsubscribe(OnBattleLoaded);
            The.BattleScene.Timer.OnTimerUpdated.Subscribe(UpdateTime);
            The.World.OnWindChange.Subscribe(UpdateWind);
        }


        private void UpdateTime (Time t) {
            string turnTime = t.ToString();
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
            if      (wind < 0) _wind.text = string.Format("<- {0:F1}", -wind);
            else if (wind > 0) _wind.text = string.Format("{0:F1} ->",  wind);
            else               _wind.text = "0.0";
        }

    }

}
