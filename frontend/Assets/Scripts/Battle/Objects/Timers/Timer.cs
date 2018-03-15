using Core;


namespace Battle.Objects.Timers {

    public class Timer : Component {

        protected Time Time;


        public Timer (Time time) {
            Time = time;
        }


        public void Update () {
            if (--Time.Ticks > 0) {
                OnTick();
                return;
            }
            OnTick();
            OnExpire();
        }


        protected virtual void OnExpire () {
            Object.Timer = null;
        }


        protected virtual void OnTick () {}

    }

}
