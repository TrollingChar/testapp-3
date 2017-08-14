using System;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;
using War;


namespace UI.Panels {

    public class BottomHud : Panel {

        [SerializeField] private Text _middleText;
        [SerializeField] private Text _time;

        private GameStateController _state;
        private string _turnTime = "";
        private string _gameTime = "";


        private void Start () {
            _state = The<GameStateController>.Get();
            
            _state.OnTimerUpdated.Subscribe(UpdateTime);
        }


        private void UpdateTime (int time) {
            string turnTime = ((time + 999) / 1000).ToString();
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
