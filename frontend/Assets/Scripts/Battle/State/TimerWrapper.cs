﻿using Utils.Messenger;
using Utils.Singleton;


namespace Battle.State {

    public class TimerWrapper {

        private const int TurnTime = 10000;
        private const int RetreatTime = 3000;
        public readonly Messenger OnTimerElapsed = new Messenger();

        public readonly Messenger<int> OnTimerUpdated = new Messenger<int>();
        private bool _frozen; // todo: access it from weapon

        private int _time;


        public TimerWrapper () {
            The<TimerWrapper>.Set(this);
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
            //if (_battle.State.Is(GameState.Turn)) return;
            if (Time < milliseconds) Time = milliseconds;
        }


        public void Update () {
            if (_frozen) return;
            Time -= 20;
            if (HasElapsed) OnTimerElapsed.Send();
        }

    }

}
