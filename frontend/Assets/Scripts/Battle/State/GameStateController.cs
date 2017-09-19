using Battle.Teams;
using Commands.Server;
using Core;
using Messengers;
using Net;
using UnityEngine;
using Utils.Singleton;

namespace Battle.State {

    public partial class GameStateController {

        private readonly WSConnection _connection;


        public GameStateController () {
            The<GameStateController>.Set(this);

            _connection = The<WSConnection>.Get();
            _playerId = The<PlayerInfo>.Get().Id;

            OnTimerUpdated = new TimerUpdatedMessenger();

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


        private void Hint (string text) {
            Debug.Log(text);
            The<BattleScene>.Get().ShowHint(text);
        }

    }

}
