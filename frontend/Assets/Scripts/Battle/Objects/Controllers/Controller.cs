using Battle.Experimental;
using Battle.State;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class Controller : Component {

        private readonly NewTimer _timer = The.Battle.TweenTimer;


        public void Update (TurnData td) {
            DoUpdate (td);
        }


        protected virtual void DoUpdate (TurnData td) {}


        protected void Wait () {
            _timer.Wait (new Time {Seconds = 0.5f});
        }


        protected void Wait (Time t) {
            _timer.Wait (t);
        }

    }

}