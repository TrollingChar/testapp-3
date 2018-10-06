using System;
using Core;
using Utils;


namespace Battle.State {

    public class TimerWrapper {

        public event Action        OnTimerElapsed;
        public event Action <Time> OnTimerUpdated;

        public bool Frozen { get; set; }

        private Time _time;


        public TimerWrapper () {
            The.TimerWrapper = this;
        }


        public Time Time {
            get { return _time; }
            set { OnTimerUpdated._ (_time = value); }
        }


        public int Ticks {
            get { return _time.Ticks; }
            set {
                _time.Ticks = value;
                OnTimerUpdated._ (_time);
            }
        }


        public float Seconds {
            get { return _time.Seconds; }
            set {
                _time.Seconds = value;
                OnTimerUpdated._ (_time);
            }
        }


        public bool HasElapsed {
            get { return Time.Ticks <= 0; }
        }


        public void Wait () {
            if (The.GameState.CurrentState != GameState.Turn && Seconds < 0.5f) {
                Seconds = 0.5f;
            }
        }


        public void Wait (Time t) {
            if (The.GameState.CurrentState != GameState.Turn && Time < t) {
                Time = t;
            }
        }


        public void Update () {
            if (Frozen) return;
            Ticks--;
            if (HasElapsed) OnTimerElapsed._ ();
        }

    }

}