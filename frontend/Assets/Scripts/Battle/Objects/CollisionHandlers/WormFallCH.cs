using Battle.Objects.Controllers;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {

        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var o = c.Collider2.Object;
//            var worm = c.Collider2.Object as Worm;
//            if (worm == null) return;
//            if (worm.Controller is WormWalkCtrl) worm.Controller = new WormFallCtrl();
            if (o is Worm) {
                o.Controller = new WormFallCtrl();
            }
            else if (o.Controller is LandmineStuckCtrl) {
                // todo refactor?
                o.Controller = new LandmineCtrl();
                o.CollisionHandler = new CollisionHandler();
            }
        }

    }

}
