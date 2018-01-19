using Battle.State;
using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;
using Time = Core.Time;


namespace Battle.UI {

    public class BottomHud : Panel {

        private BattleScene _battleScene;

        private string _gameTime = "";

        [SerializeField] private Text _middleText;

        private GameStateController _state;
        [SerializeField] private Text _time;
        private string _turnTime = "";


        private void Awake () {
            _battleScene = The.BattleScene;
            _battleScene.OnBattleLoaded.Subscribe(OnBattleLoaded);
        }


        private void OnBattleLoaded () {
            _battleScene.OnBattleLoaded.Unsubscribe(OnBattleLoaded);
            _battleScene.Timer.OnTimerUpdated.Subscribe(UpdateTime);
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

    }

}
