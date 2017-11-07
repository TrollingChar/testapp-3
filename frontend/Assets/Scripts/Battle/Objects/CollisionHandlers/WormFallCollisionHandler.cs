using System;
using Battle.Objects.Controllers;
using Battle.Physics.Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision(Collision c)
        {
            return Object.Velocity.SqrLength <= 4f || !(
                c.Collider2 != null
                && c.Collider2.Object is Worm
                && !(c.Collider2.Object.Controller is WormControllerFall)
            );
        }


        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var worm = c.Collider2.Object as Worm;
            if (worm == null) return;
            worm.Controller = new WormControllerFall();
        }

    }

}
