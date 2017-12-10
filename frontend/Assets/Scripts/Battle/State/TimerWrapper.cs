using Core;
using Utils.Messenger;


namespace Battle.State {

    public class TimerWrapper {

        public readonly Messenger OnTimerElapsed = new Messenger();
        public readonly Messenger<int> OnTimerUpdated = new Messenger<int>();
        public bool Frozen { get; set; }

        private int _time;


        public TimerWrapper () {
            The.TimerWrapper = this;
        }


        public int Time {
            get { return _time; }
            set {
                _time = value;
                OnTimerUpdated.Send(value);
            }
        }

        public bool HasElapsed {
            get { return Time <= 0; }
        }


        public void Wait (int milliseconds) {
//            if (_battle.State.Is(GameState.Turn)) return;
            if (Time < milliseconds) Time = milliseconds;
        }


        public void Update () {
            if (Frozen) return;
            Time -= 20;
            if (HasElapsed) OnTimerElapsed.Send();
        }

    }

}
