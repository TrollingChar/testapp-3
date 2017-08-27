using Battle.Objects;
using Battle.Teams;
using Battle.Weapons;
using Core;
using Messengers;
using Net;
using UnityEngine;
using Utils.Random;
using Utils.Singleton;


namespace Battle.State {

    public class GameStateController {

        private const int TurnTime = 10000;
        private const int RetreatTime = 3000;

        private readonly WSConnection _connection;

        private GameState _next;
        private readonly int _id;

        private int _time;

        private Worm _worm;
        private bool _wormFrozen;

        private Weapon _weapon;

        public int ActivePlayer;

        public bool Synchronized;


        public GameStateController () {
            The<GameStateController>.Set(this);

            _connection = The<WSConnection>.Get();
            _id = The<PlayerInfo>.Get().Id;

            OnTimerUpdated = new TimerUpdatedMessenger();

            CurrentState = GameState.AfterTurn;
            Hint("AFT");
            _next = GameState.Remove0Hp;
            Timer = 500;
            _wormFrozen = false;
            Worm = null;

            _connection.OnNewTurn.Subscribe(OnNewTurn); // todo unsubscribe when battle ends
        }


        public TimerUpdatedMessenger OnTimerUpdated { get; private set; }

        public GameState CurrentState { get; private set; }

//        private readonly BF _bf = The<BF>.Get();
//        private readonly CoreEvents _coreEvents = The<CoreEvents>.Get();

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
                OnTimerUpdated.Send(value);
            }
        }


        public bool WormFrozen { get; private set; }


        public bool IsMyTurn {
            get { return ActivePlayer == _id && CurrentState == GameState.Turn; }
        }


        public void Update () {
            if ((Timer -= 20) <= 0) ChangeState();
        }


        public void Wait (int milliseconds) {
            if (CurrentState == GameState.Turn) return;
            if (Timer < milliseconds) Timer = milliseconds;
        }


        public void OnNewTurn (int id) {
            ActivePlayer = id;
            Worm = The<TeamManager>.Get().Teams[id].NextWorm(); // todo: remove chain
//            _camera.LookAt(Worm.Position);
            ChangeState();
        }


        private void Hint (string text) {
            Debug.Log(text);
            The<BattleScene>.Get().ShowHint(text);
        }


        public void SelectWeapon (int id) {
            // if we can select weapon then arm active worm with it!
            _weapon = null; //_factory.CreateWeapon(id, _worm); // 0 - select none
        }


        private void ChangeState () {
            switch (CurrentState = _next++) {

                case GameState.BeforeTurn:
                    Hint("BEF");
                    // Crates fall from the sky, regeneration works, shields replenish
                    if (RNG.Bool(0)) {
                        // drop crates
                        Timer = 500;
                    } else {
                        ChangeState();
                    }
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
                    } else {
                        ChangeState();
                    }
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
