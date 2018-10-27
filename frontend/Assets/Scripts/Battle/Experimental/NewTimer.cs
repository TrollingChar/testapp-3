using System;
using Core;
using Utils;


namespace Battle.Experimental {

    public class NewTimer {

        private Time _time;
        private bool _paused;

        public event Action <Time> OnChanged;
        public event Action <bool> OnPauseChanged;


        public bool Paused {
            get { return _paused; }
            set { OnPauseChanged._ (_paused = value); }
        }


        public bool Elapsed { get { return Ticks <= 0; } }


        public int Ticks {
            get { return _time.Ticks; }
            set {
                _time.Ticks = value;
                OnChanged._ (_time);
            }
        }


        public float Seconds {
            get { return _time.Seconds; }
            set {
                _time.Seconds = value;
                OnChanged._ (_time);
            }
        }


        public Time Time {
            get { return _time; }
            set { OnChanged._ (_time = value); }
        }


        public void Wait () {
            Wait (new Time {Seconds = 1});
        }


        public void Wait (Time time) {
            if (time > _time) Time = time;
        }


        public void Update () {
            if (Elapsed || Paused) return;
            Ticks--;
        }

    }

}