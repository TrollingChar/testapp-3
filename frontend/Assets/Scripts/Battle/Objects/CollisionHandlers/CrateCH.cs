using Battle.Experimental;
using Battle.Objects.Other.Crates;
using Battle.State;
using Core;
using Collision = Collisions.Collision;


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