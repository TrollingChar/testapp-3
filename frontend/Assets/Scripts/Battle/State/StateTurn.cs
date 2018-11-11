using Battle.Experimental;
using Core;


namespace Battle.State {

    public class TurnState : NewGameState {

        private readonly BattleScene _battle = The.Battle;


        public override void Init () {
            _battle.TurnTimer.Paused = false;
        }


        public override NewGameState Next () {
            return _battle.TurnTimer.Elapsed ? new EndingTurnState () : null;
        }


        public override void Update () {
            _battle.SyncUpdateWorld ();
        }

    }

}