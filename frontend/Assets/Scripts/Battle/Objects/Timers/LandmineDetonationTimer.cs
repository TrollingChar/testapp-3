using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Timers {

    public class LandmineDetonationTimer : DetonationTimer {

        private Animator _animator;


        public LandmineDetonationTimer (Time time) : base (time) {}


        public override void OnAdd () {
            _animator = Object.GameObject.GetComponentInChildren <Animator> ();
        }


        protected override void OnTick () {
            base.OnTick ();
            float seconds = Time.Seconds;
            if (seconds > 1) {
                int i = Mathf.CeilToInt (seconds * 4);
                _animator.SetInteger ("Frame", 2 - (i & 1));
            }
            else {
                int i = Mathf.CeilToInt (seconds * 12);
                _animator.SetInteger ("Frame", 2 - (i & 1));
            }
        }

    }

}