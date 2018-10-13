using Core;


namespace Battle.Experimental {

    public class ReadyState : NewGameState {

        public override void Init () {
            The.Battle.TweenTimer.Seconds = 5;
            The.Battle.TurnTimer.Seconds = 30;
            The.Battle.TurnTimer.Paused = true;
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed || InputAvailable ? new TurnState () : null;
        }


        public override void Update () {
            The.Battle.UpdateWorld ();
        }

    }

}