using System;
using Core;
using UnityEngine;


namespace Battle.State {

    public class GameStateController {

        private readonly BattleScene _battle;

        private GameState _currentState;


        public GameStateController () {
            The.GameState = this;

            _battle = The.BattleScene;

            CurrentState = GameState.AfterTurn;
            NextState = GameState.Remove0Hp;
        }


        private GameState NextState { get; set; }


        public GameState CurrentState {
            get { return _currentState; }
            private set {
                _currentState = value;
                Debug.Log(value.ToString());
                _battle.ShowHint(value.ToString());
            }
        }


        public void ChangeState () {
            var timer = _battle.Timer;
            timer.Ticks = 0;
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
                    _battle.NewTurn(); // - commented - this is called from BattleScene
//                    StartTurn();
                    return;
                case GameState.EndingTurn:
                    _battle.TurnEnded();
//                    FreezeWorm();
                    break;
                case GameState.AfterTurn:
                    _battle.AfterTurn();
//                    PoisonDamage();
                    break;
                case GameState.Remove0Hp:
                    _battle.Remove0Hp();
                    NextState = timer.HasElapsed ? GameState.BeforeTurn : GameState.Remove0Hp; // special case
                    break;
                default: throw new ArgumentOutOfRangeException();
            } while (timer.HasElapsed);
        }

    }

}
