using Battle.Objects.Projectiles;
using Battle.State;
using Core;
using DataTransfer.Data;
using UnityEngine;


namespace Battle.Objects.Controllers {

    public class Controller : Component {

        private readonly TimerWrapper _gameTimer = The.TimerWrapper;
        private int _timer = 20000;

        public int Timer {
            get { return _timer; }
            set { _timer = value; }
        }


        public void Update (TurnData td) {
            if (Timer > 0 & (Timer -= 20) <= 0) {
                DoUpdate(td);
                OnTimer();
            } else {
                DoUpdate(td);
            }
        }


        protected virtual void DoUpdate (TurnData td) {}


        public virtual void OnTimer () {}


        protected void Wait (int milliseconds = 500) {
            _gameTimer.Wait(milliseconds);
        }

    }

}
