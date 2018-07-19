using Battle.State;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class Controller : Component {

        private readonly TimerWrapper _gameTimer = The.TimerWrapper;


        public void Update (TurnData td) {
            DoUpdate(td);
        }


        protected virtual void DoUpdate (TurnData td) {}


        protected void Wait () {
            _gameTimer.Wait(new Time {Seconds = 0.5f});
        }


        protected void Wait (Time t) {
            _gameTimer.Wait(t);
        }

    }

}
