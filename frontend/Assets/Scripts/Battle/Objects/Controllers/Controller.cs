using Battle.State;
using Utils.Singleton;


namespace Battle.Objects.Controllers {

    public class Controller : Component {

        private readonly GameStateController _state = The<GameStateController>.Get();
        private readonly TimerWrapper _timer = The<TimerWrapper>.Get();

        public virtual void Update (TurnData td) {}


        protected void Wait (int milliseconds = 500) {
            _timer.Wait(milliseconds);
        }

    }

}
