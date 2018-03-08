using Battle.State;
using Core;


namespace Battle.Objects.Timers {

    public class LandmineInitialTimer : Timer {

        private readonly TimerWrapper _gameTimer = The.TimerWrapper;
        
        
        public LandmineInitialTimer () : base(new Time{Seconds = 2}) {}


        protected override void OnTick () {
            _gameTimer.Wait();
        }
    }

}
