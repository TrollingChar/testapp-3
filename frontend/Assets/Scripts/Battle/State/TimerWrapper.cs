using Core;
using Utils.Messenger;


namespace Battle.State {

    public class TimerWrapper {

        public readonly Messenger OnTimerElapsed = new Messenger();
        public readonly Messenger<Time> OnTimerUpdated = new Messenger<Time>();
        public bool Frozen { get; set; }

        private Time _time;


        public TimerWrapper () {
            The.TimerWrapper = this;
        }


        public Time Time {
            get { return _time; }
            set {
                _time = value;
                OnTimerUpdated.Send(_time);
            }
        }

        public int Ticks {
            get { return _time.Ticks; }
            set {
                _time.Ticks = value;
                OnTimerUpdated.Send(_time);
            }
        }

        public float Seconds {
            get { return _time.Seconds; }
            set {
                _time.Seconds = value;
                OnTimerUpdated.Send(_time);
            }
        }

        public bool HasElapsed {
            get { return Time.Ticks <= 0; }
        }


        public void Wait (Time t) {
            if (The.GameState.CurrentState != GameState.Turn && Time < t) {
                Time = t;
            }
        }


        public void Update () {
            if (Frozen) return;
            Ticks--;
            if (HasElapsed) OnTimerElapsed.Send();
        }

    }

}
