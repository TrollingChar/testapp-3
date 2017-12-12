using Battle.Objects.Controllers;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (NewCollision c) {
            return Object.Velocity.SqrLength < 1f || !(
                c.Collider2 != null
                && c.Collider2.Object.Controller is WormControllerWalk
            );
        }


        public override void OnCollision (NewCollision c) {
            if (c.Collider2 == null) return;
            var worm = c.Collider2.Object as Worm;
            if (worm == null) return;
            if (worm.Controller is WormControllerWalk) worm.Controller = new WormControllerFall();
        }

    }

}
