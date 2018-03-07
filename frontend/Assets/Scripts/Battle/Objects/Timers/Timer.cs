using Core;


namespace Battle.Objects.Timers {

    public abstract class Timer : Component {

        private Time _time;


        public Timer (Time time) {
            _time = time;
        }
        

        public void Update () {
            if (--_time.Ticks > 0) return;
            Object.Timer = null;
            OnExpire();
        }


        protected abstract void OnExpire ();

    }

}
