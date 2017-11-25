using Battle.Objects.Controllers;
using Battle.Physics.Collisions;
using Geometry;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision(Collision c)
        {
            return Object.Velocity.SqrLength < 1f || !(
                c.Collider2 != null
                && c.Collider2.Object.Controller is WormControllerWalk
            );
        }


        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var worm = c.Collider2.Object as Worm;
            if (worm == null) return;
            if (worm.Controller is WormControllerWalk) worm.Controller = new WormControllerFall();
        }

    }

}
