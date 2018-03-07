using Core;


namespace Battle.Objects.Timers {

    public class LandmineInitialTimer : Timer {

        public LandmineInitialTimer () : base(new Time{Seconds = 2}) {}

        protected override void OnExpire () {
            throw new System.NotImplementedException();
        }

    }

}
