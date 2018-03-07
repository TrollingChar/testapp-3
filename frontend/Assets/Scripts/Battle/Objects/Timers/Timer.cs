using Core;


namespace Battle.Objects.Timers {

    public abstract class Timer : Component {

        protected Time Time;


        protected Timer (Time time) {
            Time = time;
        }


        public void Update () {
            if (--Time.Ticks > 0) {
                OnTick();
                return;
            }
            OnTick();
            Object.Timer = null;
            OnExpire();
        }


        protected abstract void OnExpire ();
        protected virtual void OnTick () {}

    }

}
