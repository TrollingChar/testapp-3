using Core;
using DataTransfer.Client;
using Geometry;
using Utils.Random;


namespace Battle.Experimental {

    public class BeforeTurnState : NewGameState {


        public override void Init () {
            The.Battle.Synchronized = false;
            The.Connection.Send (new TurnEndedCmd (The.Battle.Teams.MyTeam.WormsAlive > 0));
            
            for (int i = 0, count = The.Battle.Teams.Teams.Count; i < count; i++) {
                The.Battle.Teams.NextTeam ();
                // todo drop NUKES
                if (The.Battle.Teams.ActiveTeam.WormsAlive > 0) {
                    // todo extinguish flames
        
                    The.World.Wind = RNG.Float () * 10f - 5f;
                    
                    The.Battle.TweenTimer.Wait ();
        
                    var crate = The.Battle.CrateFactory.GenCrate ();
                    if (crate == null) return;
                    var xy = new XY (RNG.Float () * The.World.Width, The.World.Height + 500);
                    The.World.Spawn (crate, xy);
                    return;
                }
            }
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed ? new SynchronizingState () : null;
        }


        public override void Update () {
            The.Battle.UpdateWorld ();
        }

    }

}