using Utils;
using War.Objects;


namespace War {

    public enum GameState {

        BeforeTurn,
        Synchronizing,
        Turn,
        EndingTurn,
        AfterTurn,
        Remove0Hp

    }


    public class GameStateController {

        private const int TurnTime = 999000;
        private const int RetreatTime = 3000;

        public bool Synchronized;

        private GameState _current, _next;

        public GameState CurrentState {
            get { return _current; }
        }

        public int ActivePlayer;

        private bool _wormFrozen;
        public Worm Worm;

        private int _time;

        public int Timer {
            get { return _time; }
            set {
                _time = value;
                Core.CoreEvents.SetTurnTime.Invoke(TimerString);
            }
        }

        public string TimerString {
            get { return ((_time + 999) / 1000).ToString(); }
        }


        public GameStateController () {
            _current = GameState.AfterTurn;
            Hint("AFT");
            _next = GameState.Remove0Hp;
            Timer = 500;
            _wormFrozen = false;
            Worm = null;
        }


        public void Update () {
            if ((Timer -= 20) <= 0) ChangeState();
        }


        public void Wait (int milliseconds) {
            if (_current == GameState.Turn) return;
            if (Timer < milliseconds) Timer = milliseconds;
        }


        public void StartTurn (int id) {
            ActivePlayer = id;
            ChangeState();
        }


        private void Hint (string text) {
            Core.CoreEvents.SetGameTime.Invoke(text);
        }


        private void ChangeState () {
            switch (_current = _next++) {
                case GameState.BeforeTurn:
                    Hint("BEF");
                    // Crates fall from the sky, regeneration works, shields replenish
                    if (RNG.Bool(0)) {
                        // drop crates
                        Timer = 500;
                    } else ChangeState();
                    break;
                case GameState.Synchronizing:
                    Hint("SYN");
                    // Game sends pos0 signal and waits until server receives all signals
                    Synchronized = true;
                    Core.Synchronize();
                    break;
                case GameState.Turn:
                    Hint(ActivePlayer == Core.Id ? "MY" : "TURN");
                    // Player moves his worm and uses weapon
                    _wormFrozen = false;
                    Worm = Core.BF.NextWorm();
                    Timer = TurnTime;
                    break;
                case GameState.EndingTurn:
                    Hint("END");
                    // Player ended his turn, but projectiles still flying
                    _wormFrozen = true;
                    Synchronized = false;
                    Worm = null;
                    //Core.bf.ResetActivePlayer();
                    Timer = 500;
                    break;
                case GameState.AfterTurn:
                    Hint("AFT");
                    // Poisoned worms take damage
                    if (RNG.Bool(0)) {
                        // poison damage
                        Timer = 500;
                    } else ChangeState();
                    break;
                case GameState.Remove0Hp:
                    Hint("REM");
                    _next = GameState.BeforeTurn; // no overflow
                    // Worms with 0 HP explode
                    if (RNG.Bool(0)) {
                        // blow them up
                        _next = GameState.Remove0Hp;
                        Timer = 500;
                    }
                    ChangeState();
                    break;
                default:
                    Hint("ERR");
                    break;
            }
        }

    }

}
