using Utils.Messenger;

namespace Battle.State
{
    public class TimerWrapper
    {
        private const int TurnTime = 10000; 
        private const int RetreatTime = 3000; 
        
        private int _time;
        private bool _frozen;
        
        public readonly Messenger<int> OnTimerUpdated = new Messenger<int>();
        public readonly Messenger OnTimerElapsed = new Messenger();

        public int Time
        {
            get { return _time; }
            set {
                _time = value;
                OnTimerUpdated.Send(value);
            }
        }

        public void Wait(int milliseconds)
        {
            //if (_battle.State.Is(GameState.Turn)) return;
            if (Time < milliseconds) Time = milliseconds;
        }

        public void Update()
        {
            if (_frozen) return;
            Time -= 20;
            if (HasElapsed) OnTimerElapsed.Send();
        }

        public bool HasElapsed
        {
            get { return Time <= 0; }
        }
    }
}