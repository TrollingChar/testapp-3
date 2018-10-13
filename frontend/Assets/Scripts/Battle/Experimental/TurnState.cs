using Core;


namespace Battle.Experimental {

    public class TurnState : NewGameState {

        public override void Init () {
            The.Battle.TurnTimer.Paused = false;
        }


        public override NewGameState Next () {
            return The.Battle.TurnTimer.Elapsed ? new EndingTurnState () : null;
        }


        public override void Update () {
            The.Battle.SyncUpdateWorld ();
        }

    }

}