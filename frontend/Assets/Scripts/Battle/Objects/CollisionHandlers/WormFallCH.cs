using Battle.Objects.Controllers;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            return Object.Velocity.SqrLength < 1f || !(
                c.Collider2 != null
                && c.Collider2.Object.Controller is WormControllerWalk
            );
        }


        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var o = c.Collider2.Object;
//            var worm = c.Collider2.Object as Worm;
//            if (worm == null) return;
//            if (worm.Controller is WormControllerWalk) worm.Controller = new WormControllerFall();
            if (o.Controller is WormControllerWalk ||
                o.Controller is WormControllerJump) {
                o.Controller = new WormControllerFall();
            }
            else if (o.Controller is LandmineControllerStuck) {
                // todo refactor?
                o.Controller = new LandmineController();
                o.CollisionHandler = new CollisionHandler();
            }
        }

    }

}
