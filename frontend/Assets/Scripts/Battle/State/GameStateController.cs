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
        private readonly int _playerId;

        private GameState _currentState;
        private GameState NextState { get; set; }

        public readonly Messenger<int> OnTimerUpdated = new Messenger<int>();
        
        public GameStateController()
        {
            The<GameStateController>.Set(this);

            _playerId = The<PlayerInfo>.Get().Id;

            CurrentState = GameState.AfterTurn;
            NextState = GameState.Remove0Hp;
            _timer.Time = 500;
            
            CommandExecutor<StartNewTurnCmd>.AddHandler(OnNewTurn); // todo unsubscribe when battle ends
        }

        public GameState CurrentState
        {
            get { return _currentState; }
            private set
            {
                _currentState = value;
                Debug.Log(value.ToString());
                The<BattleScene>.Get().ShowHint(value.ToString());
            }
        }


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

//        public readonly Messenger<TimerWrapper> OnBeforeTurnPhase;
//        public readonly Messenger OnSynchroPhase;
//        public readonly Messenger<TimerWrapper> OnTurnPhase;
//        public readonly Messenger<TimerWrapper> OnEndTurnPhase;
//        public readonly Messenger<TimerWrapper> OnAfterTurnPhase;
//        public readonly Messenger<TimerWrapper> OnRemove0HpPhase;

        private BattleScene _battle;

        public void ChangeState()
        {
            var _timer = _battle.Timer;
            _timer.Time = 0;
            do switch (CurrentState = NextState++) {
                case GameState.BeforeTurn:
                    _battle.BeforeTurn();
//                    DropCrates();
//                    RegenHp();
//                    RenewShields();
                    break;
                case GameState.Synchronizing:
                    _battle.Synchronize();
                    // special case
//                    SendSyncData();
                    return;
                case GameState.Turn:
                    _battle.NewTurn();
//                    StartTurn();
                    break;
                case GameState.EndingTurn:
                    _battle.EndTurn();
//                    FreezeWorm();
                    break;
                case GameState.AfterTurn:
                    _battle.AfterTurn();
//                    PoisonDamage();
                    break;
                case GameState.Remove0Hp:
                    _battle.Remove0Hp();
//                    Remove0Hp();
                    NextState = _timer.HasElapsed ? GameState.BeforeTurn : GameState.Remove0Hp; // special case
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            } while (_timer.HasElapsed);
        }
    }
}