using Battle.Objects.Controllers;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            return Object.Velocity.SqrLength < 1f || !(
                c.Collider2 != null
                && c.Collider2.Object.Controller is WormWalkCtrl
            );
        }


        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var o = c.Collider2.Object;
//            var worm = c.Collider2.Object as Worm;
//            if (worm == null) return;
//            if (worm.Controller is WormWalkCtrl) worm.Controller = new WormControllerFall();
            if (o.Controller is WormWalkCtrl ||
                o.Controller is WormJumpCtrl) {
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
