using System;
using Battle.State;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class Controller : Component {

        private readonly TimerWrapper _gameTimer = The.TimerWrapper;
        [Obsolete] public Time Timer = new Time {Seconds = 20};


        public void Update (TurnData td) {
            if (--Timer.Ticks == 0) {
                DoUpdate(td);
                OnTimer();
            } else {
                DoUpdate(td);
            }
        }


        protected virtual void DoUpdate (TurnData td) {}


        public virtual void OnTimer () {}


        protected void Wait () {
            _gameTimer.Wait(new Time {Seconds = 0.5f});
        }


        protected void Wait (Time t) {
            _gameTimer.Wait(t);
        }

    }

}
