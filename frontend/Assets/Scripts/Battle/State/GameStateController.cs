using System;
using System.Runtime.InteropServices;
using Battle.Objects;
using Battle.Teams;
using Battle.Weapons;
using Commands.Client;
using Commands.Server;
using Core;
using UnityEngine;
using Utils.Messenger;
using Utils.Random;
using Utils.Singleton;

namespace Battle.State
{
    public class GameStateController
    {
        // todo: remove from there, it's moved to TimerWrapper

        private const int TurnTime = 10000;
        private const int RetreatTime = 3000;

        private readonly int _playerId;

        private GameState _currentState;
        private GameState NextState { get; set; }

//        private int _time;
        
        // todo: remove from there, it's moved to WeaponWrapper
        // todo: use arsenal
//        private Weapon _weapon;
        
        // todo: move to ActiveWormWrapper
//        private Worm _worm;
//        public int ActivePlayer;

        public readonly Messenger<int> OnTimerUpdated = new Messenger<int>();
        
        public GameStateController()
        {
            The<GameStateController>.Set(this);

            _playerId = The<PlayerInfo>.Get().Id;

            CurrentState = GameState.AfterTurn;
            NextState = GameState.Remove0Hp;
            Timer = 500;
            
            CommandExecutor<StartNewTurnCmd>.AddHandler(OnNewTurn); // todo unsubscribe when battle ends
        }

//        public bool WormFrozen { get; private set; }
//        public Worm Worm
//        {
//            get { return _worm; }
//            private set
//            {
//                if (_worm != null) {
//                    _worm.ArrowVisible = false;
//                }
//                _worm = value;
//                if (value == null) {
//                    return;
//                }
//                _worm.ArrowVisible = true;
//            }
//        }

//        public int Timer
//        {
//            get { return _time; }
//            set
//            {
//                _time = value;
//                OnTimerUpdated.Send(value);
//            }
//        }

        public GameState CurrentState
        {
            get { return _currentState; }
            private set
            {
                _currentState = value;
                Debug.Log("AYUOY");
                The<BattleScene>.Get().ShowHint("AYUOY");
            }
        }

//        [Obsolete]
//        public bool IsMyTurn
//        {
//            get { return ActivePlayer == _playerId && CurrentState == GameState.Turn; }
//        }


//        private void OnNewTurn(StartNewTurnCmd startNewTurnCommand)
//        {
            // too many references from here
//            int id = startNewTurnCommand.Player;
//            ActivePlayer = id;
//            Worm = The<TeamManager>.Get().Teams[id].NextWorm(); // todo: remove chain
//            _camera.LookAt(Worm.Position);
//            PrepareWeapon(0);
//            CanSelectWeapon = true;
//            ChangeState();
//        }

//        public void Wait(int milliseconds)
//        {
//            if (CurrentState == GameState.Turn) {
//                return;
//            }
//            if (Timer < milliseconds) {
//                Timer = milliseconds;
//            }
//        }


//        public void Update()
//        {
//            if ((Timer -= 20) <= 0) {
//                ChangeState();
//            }
//        }

        public readonly Messenger<TimerWrapper> OnBeforeTurnPhase;
        public readonly Messenger OnSynchroPhase;
        public readonly Messenger<TimerWrapper> OnTurnPhase;
        public readonly Messenger<TimerWrapper> OnEndTurnPhase;
        public readonly Messenger<TimerWrapper> OnAfterTurnPhase;
        public readonly Messenger<TimerWrapper> OnRemove0HpPhase;

        public void ChangeState()
        {
            _timer.Time = 0;
            do switch (CurrentState = NextState++) {
                case GameState.BeforeTurn:
                    OnBeforeTurnPhase.Send(_timer);
//                    DropCrates();
//                    RegenHp();
//                    RenewShields();
                    break;
                case GameState.Synchronizing:
                    // special case
                    OnSynchroPhase.Send();
//                    SendSyncData();
                    return;
                case GameState.Turn:
                    OnTurnPhase.Send(_timer);
//                    StartTurn();
                    break;
                case GameState.EndingTurn:
                    OnEndTurnPhase.Send(_timer);
//                    FreezeWorm();
                    break;
                case GameState.AfterTurn:
                    OnAfterTurnPhase.Send(_timer);
//                    PoisonDamage();
                    break;
                case GameState.Remove0Hp:
                    OnRemove0HpPhase.Send(_timer);
//                    Remove0Hp();
                    NextState = _timer.HasElapsed ? GameState.BeforeTurn : GameState.Remove0Hp; // special case
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            } while (_timer.HasElapsed);
            return;
            
            
            
            
            
            
            switch (CurrentState = NextState++) {
                
                // state changes when timer=0
                
                case GameState.BeforeTurn:
                    // Crates fall from the sky, regeneration works, shields replenish
                    if (RNG.Bool(0)) {
                        DropCrates();
                        Timer = 500;
                    } else {
                        ChangeState();
                    }
                    break;

                case GameState.Synchronizing:
                    // Game sends a signal and waits until server receives all signals
                    new EndTurnCmd(true).Send();
                    break;

                case GameState.Turn:
                    OnTurnPhase();
                    WormFrozen = false;
//                    \_ Worm = Core.BF.NextWorm();
                    Timer = TurnTime;
                    
                    break;

                case GameState.EndingTurn:
                    OnEndingTurnPhase();
                    // Player ended his turn, but projectiles still flying
                    WormFrozen = true;
                    Worm = null;
                    //Core.bf.ResetActivePlayer();
                    Timer = 500;
                    break;

                case GameState.AfterTurn:
                    OnAfterTurnPhase();
                    // Poisoned worms take damage
                    if (RNG.Bool(0)) {
                        // poison damage
                        Timer = 500;
                    } else {
                        ChangeState();
                    }
                    break;

                case GameState.Remove0Hp:
                    NextState = GameState.BeforeTurn; // no overflow
                    OnRemove0HpPhase();
                    // Worms with 0 HP explode
                    if (RNG.Bool(0)) {
                        // blow them up
                        NextState = GameState.Remove0Hp;
                        Timer = 500;
                    }
                    ChangeState();
                    break;

                default:
                    Debug.Log("ERR");
                    The<BattleScene>.Get().ShowHint("ERR");
                    break;
            }
        }
    }
}