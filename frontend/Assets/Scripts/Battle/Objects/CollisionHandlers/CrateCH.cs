using Battle.Experimental;
using Battle.Objects.Other.Crates;
using Collisions;
using Core;


namespace Battle.Objects.CollisionHandlers {

    public class CrateCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            if (c.IsLandCollision) return;

            var worm = c.Collider2.Object as Worm;

            if (worm == The.Battle.ActiveWorm && The.Battle.State is TurnState) {
                ((Crate) Object).CollectBy (worm);
            }
        }

    }

}