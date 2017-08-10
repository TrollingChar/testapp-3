using Net;
using UI;
using UnityEngine;
using Utils;
using Utils.Singleton;
using War.Camera;
using War.Objects;
using Zenject;


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

        [Inject] private WSConnection _connection;
        [Inject] private int _id;
        [Inject] private CameraWrapper _camera;

        private const int TurnTime = 30000;
        private const int RetreatTime = 3000;

        public bool Synchronized;

        private GameState _current, _next;

        public GameState CurrentState {
            get { return _current; }
        }

        public int ActivePlayer;

        private int _time;
        private Worm _worm;
        private bool _wormFrozen;

        private readonly BF _bf = The<BF>.Get();
        private readonly CoreEvents _coreEvents = The<CoreEvents>.Get();

        public Worm Worm {
            get { return _worm; }
            private set {
                if (_worm != null) _worm.ArrowVisible = false;
                if ((_worm = value) == null) return;
                _worm.ArrowVisible = true;
            }
        }

        public int Timer {
            get { return _time; }
            set {
                _time = value;
                _coreEvents.SetTurnTime.Invoke(TimerString);
            }
        }

        public string TimerString {
            get { return ((_time + 999) / 1000).ToString(); }
        }

        public bool WormFrozen { get; private set; }


        public bool IsMyTurn {
            get { return ActivePlayer == _id && CurrentState == GameState.Turn; }
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
            Worm = _bf.Teams[id].NextWorm();
            _camera.LookAt(Worm.Position);
            ChangeState();
        }


        private void Hint (string text) {
            Debug.Log(text);
            _coreEvents.SetGameTime.Invoke(text);
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
                    // Game sends a signal and waits until server receives all signals
                    Synchronized = true;
                    _connection.SendEndTurn(true);
                    break;
                case GameState.Turn:
                    Hint(ActivePlayer == _id ? "MY" : "TURN");
                    // Player moves his worm and uses weapon
                    _wormFrozen = false;
                    //Worm = Core.BF.NextWorm();
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
