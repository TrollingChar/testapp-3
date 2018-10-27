using Core;


namespace Battle.Experimental {

    public class TurnState : NewGameState {

        private readonly NewBattleScene _battle = The.Battle;


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