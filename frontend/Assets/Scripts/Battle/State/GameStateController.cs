using System;
using System.Runtime.InteropServices;
using Battle.Objects;
using Battle.Teams;
using Battle.Weapons;
using Commands.Client;
using Commands.Server;
using UnityEngine;
using Utils.Messenger;
using Utils.Random;
using Utils.Singleton;

namespace Battle.State
{
    public class GameStateController
    {
        private BattleScene _battle;

        private GameState _currentState;
        private GameState NextState { get; set; }

        
        public GameStateController()
        {
            The<GameStateController>.Set(this);

            _battle = The<BattleScene>.Get();

            CurrentState = GameState.AfterTurn;
            NextState = GameState.Remove0Hp;
        }

        
        public GameState CurrentState
        {
            get { return _currentState; }
            private set
            {
                _currentState = value;
                Debug.Log(value.ToString());
                _battle.ShowHint(value.ToString());
            }
        }


        public void ChangeState()
        {
            var timer = _battle.Timer;
            timer.Time = 0;
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
//                    _battle.NewTurn(); - commented - this is called from BattleScene
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
                    NextState = timer.HasElapsed ? GameState.BeforeTurn : GameState.Remove0Hp; // special case
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            } while (timer.HasElapsed);
        }
    }
}