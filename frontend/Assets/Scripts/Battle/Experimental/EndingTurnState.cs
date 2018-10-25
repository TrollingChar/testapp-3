using Core;


namespace Battle.Experimental {

    public class EndingTurnState : NewGameState {

        public override void Init () {
            The.Battle.ActiveWorm = null;
            The.Battle.Teams.ActiveTeam.NextWorm ();
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed ? new AfterTurnState () : null;
        }


        public override void Update () {
            if (The.Battle.ControlTimer.Elapsed) {
                The.Battle.UpdateWorld ();
            }
            else {
                The.Battle.SyncUpdateWorld ();
            }
        }

    }

}