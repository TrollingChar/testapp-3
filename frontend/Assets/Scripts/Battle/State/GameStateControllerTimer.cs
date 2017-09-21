using Utils.Messenger;


namespace Battle.State {

    public partial class GameStateController {
        // todo: remove from there, it's moved to TimerWrapper

        private const int TurnTime = 10000;
        private const int RetreatTime = 3000;
        private int _time;
        public Messenger<int> OnTimerUpdated { get; private set; }

        public int Timer {
            get { return _time; }
            set {
                _time = value;
                OnTimerUpdated.Send(value);
            }
        }


        public void Wait (int milliseconds) {
            if (CurrentState == GameState.Turn) return;
            if (Timer < milliseconds) Timer = milliseconds;
        }


        public void Update () {
            if ((Timer -= 20) <= 0) ChangeState();
        }

    }

}
