using Core;


namespace Battle.Objects {

    public abstract class Timer : Component {

        private Time _time;


        public Timer (Time time) {
            _time = time;
        }
        

        public void Update () {
            if (--_time.Ticks <= 0) OnExpire();
        }


        protected abstract void OnExpire ();

    }

}
