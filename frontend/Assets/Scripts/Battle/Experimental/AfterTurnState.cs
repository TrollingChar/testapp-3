using System.Linq;
using Battle.Objects;
using Core;


namespace Battle.Experimental {

    public class AfterTurnState : NewGameState {
        
        public override void Init () {
            //debug
//            The.Battle.TweenTimer.Wait ();
            
            var worms = The.World.Objects.OfType <Worm> ().Where (w => w.Poison > 0 && !w.Despawned).ToList ();
            if (worms.Count == 0) return;
            foreach (var worm in worms) {
                worm.TakeDamage (worm.Poison);
            }
            The.Battle.TweenTimer.Wait ();
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed ? new RemovalState () : null;
        }


        public override void Update () {
            The.Battle.UpdateWorld ();
        }

    }


}