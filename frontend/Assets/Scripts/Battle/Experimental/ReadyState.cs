using Battle.Camera;
using Core;


namespace Battle.Experimental {

    public class ReadyState : NewGameState {

        public override void Init () {
            The.Battle.TweenTimer.Seconds = 5;
            The.Battle.TurnTimer.Seconds = 30;
            The.Battle.TurnTimer.Paused = true;
            var worm = The.Battle.ActiveWorm = The.Battle.Teams.ActiveTeam.ActiveWorm;
            The.Battle.UnlockArsenal ();
            
            The.Camera.Controller = new CameraController ();
            The.Camera.LookAt (worm.Position);
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed || The.Battle.InputAvailable ? new TurnState () : null;
        }


        public override void Update () {
            The.Battle.SyncUpdateWorld ();
        }

    }

}