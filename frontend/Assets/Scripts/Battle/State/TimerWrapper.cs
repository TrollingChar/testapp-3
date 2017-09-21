using Messengers;

namespace Battle.State
{
    public class TimerWrapper
    {
        private const int TurnTime = 10000; 
        private const int RetreatTime = 3000; 
        
        private int _time;
        public readonly TimerUpdatedMessenger OnTimerUpdated = new TimerUpdatedMessenger();
        public readonly TimerElapsedMessenger OnTimerElapsed = new TimerElapsedMessenger();

        public int Time
        {
            get { return _time; }
            private set
            {
                _time = value;
                OnTimerUpdated.Send(value);
            }
        }

        public void Wait(int milliseconds)
        {
            if (Time < milliseconds) Time = milliseconds;
        }

        public void Update()
        {
            Time -= 20;
            if (Time <= 0) OnTimerElapsed.Send();
        }
    }
}