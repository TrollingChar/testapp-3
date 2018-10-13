using Battle.Experimental;
using Battle.State;
using Core;


namespace Battle.Objects.Timers {

    public class LandmineInitialTimer : Timer {

        private readonly NewTimer _timer = The.Battle.TweenTimer;
        
        
        public LandmineInitialTimer () : base(new Time{Seconds = 2}) {}


        protected override void OnTick () {
            _timer.Wait();
        }
    }

}
