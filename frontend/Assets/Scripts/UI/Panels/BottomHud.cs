using Scenes;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;
using War;
using War.State;


namespace UI.Panels {

    public class BottomHud : Panel {

        private string _gameTime = "";

        [SerializeField] private Text _middleText;

        private GameStateController _state;
        [SerializeField] private Text _time;
        private string _turnTime = "";


        private void Awake () {
            The<BattleScene>.Get().OnBattleLoaded.Subscribe(OnBattleLoaded);
        }


        private void OnBattleLoaded () {
            The<BattleScene>.Get().OnBattleLoaded.Unsubscribe(OnBattleLoaded);
            _state = The<GameStateController>.Get();
            _state.OnTimerUpdated.Subscribe(UpdateTime);
        }


        private void UpdateTime (int time) {
            //string turnTime = ((time + 999) / 1000).ToString();
            string turnTime = (time / 1000f).ToString();
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
