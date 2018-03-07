using Core;


namespace Battle.Objects.Timers {

    public class DetonationTimer : Timer {

        public DetonationTimer (Time time) : base(time) {}


        protected override void OnExpire () {
            Object.Detonate();
        }

    }

}
