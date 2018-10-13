using Battle.Objects.Other.Crates;
using Core;
using DataTransfer.Client;
using Geometry;
using Utils.Random;


namespace Battle.Experimental {

    public class BeforeTurnState : NewGameState {


        public override void Init () {
            The.Battle.Teams.Next ();

            The.Connection.Send (new TurnEndedCmd (The.Battle.Teams.MyTeam.WormsAlive > 0));

            // todo extinguish flames

            The.World.Wind = RNG.Float () * 10f - 5f;

            var crate = The.Battle.CrateFactory.GenCrate ();
            if (crate == null) return;
            var xy = new XY (RNG.Float () * The.World.Width, The.World.Height + 500);
            The.World.Spawn (crate, xy);
            The.Battle.TweenTimer.Wait ();
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed ? new SynchronizingState () : null;
        }


        public override void Update () {
            The.Battle.UpdateWorld ();
        }

    }

}