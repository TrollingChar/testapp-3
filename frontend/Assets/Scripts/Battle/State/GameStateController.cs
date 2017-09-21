using System;
using Battle.Objects;
using Battle.Teams;
using Battle.Weapons;
using Commands.Client;
using Commands.Server;
using Core;
using Net;
using UnityEngine;
using Utils.Messenger;
using Utils.Random;
using Utils.Singleton;


namespace Battle.State {
    public class GameStateController {
        private readonly WSConnection _connection; // todo: move to BattleScene


        public GameStateController () {
            The<GameStateController>.Set(this);

            _connection = The<WSConnection>.Get();
            _playerId = The<PlayerInfo>.Get().Id;

            OnTimerUpdated = new Messenger<int>();

            CurrentState = GameState.AfterTurn;
            Hint("AFT");
            _next = GameState.Remove0Hp;
            Timer = 500;
            WormFrozen = false;
            Worm = null;

            CommandExecutor<StartNewTurnCmd>.AddHandler(OnNewTurn); // todo unsubscribe when battle ends
        }


        // todo: move logic to state machine
        private void OnNewTurn (StartNewTurnCmd startNewTurnCommand) {
            int id = startNewTurnCommand.Player;
            ActivePlayer = id;
            Worm = The<TeamManager>.Get().Teams[id].NextWorm(); // todo: remove chain
//            _camera.LookAt(Worm.Position);
            PrepareWeapon(0);
            CanSelectWeapon = true;

            ChangeState();
        }


        [Obsolete]
        private void Hint (string text) {
            Debug.Log(text);
            The<BattleScene>.Get().ShowHint(text);
        }
        // todo: move to ActiveWormWrapper

        private Worm _worm;
        public bool WormFrozen { get; private set; }

        public Worm Worm {
            get { return _worm; }
            private set {
                if (_worm != null) _worm.ArrowVisible = false;
                _worm = value;
                if (value == null) return;
                _worm.ArrowVisible = true;
            }
        }
        // todo: remove from there, it's moved to WeaponWrapper

        // todo: use arsenal
        private Weapon _weapon;

        public Weapon Weapon {
            get { return _weapon; }
            private set {
                _weapon = value;
                value.Equip(Worm);
            }
        }

        public int PreparedWeaponId { get; private set; }
        public bool CanSelectWeapon { get; private set; }


        public void PrepareWeapon (int id) {
            // if we can select weapon then arm active worm with it!
            PreparedWeaponId = id;
            //Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode(id); // 0 - select none
        }


        // method not used
        public void SelectWeapon (int id) {
            PreparedWeaponId = 0;
            Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode((int) PreparedWeaponId);
        }


        public void LockWeaponSelect () {
            CanSelectWeapon = false;
        }
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

        private GameState _next;
        public GameState CurrentState { get; private set; }
        public bool Synchronized; // todo: remove?


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
                    new EndTurnCmd(true).Send();
                    break;

                case GameState.Turn:
                    Hint(ActivePlayer == _playerId ? "MY" : "TURN");
                    // Player moves his worm and uses weapon
                    WormFrozen = false;
                    //Worm = Core.BF.NextWorm();
                    Timer = TurnTime;
                    break;

                case GameState.EndingTurn:
                    Hint("END");
                    // Player ended his turn, but projectiles still flying
                    WormFrozen = true;
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

        private readonly int _playerId;
        public int ActivePlayer;

        public bool IsMyTurn {
            get { return ActivePlayer == _playerId && CurrentState == GameState.Turn; }
        }
    }
}
