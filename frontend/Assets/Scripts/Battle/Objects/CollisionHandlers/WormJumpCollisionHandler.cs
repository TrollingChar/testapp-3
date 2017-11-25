using Battle.Objects.Controllers;
using Collision = Geometry.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            // wtf
            return c.Collider1 != ((Worm) Object).Tail
                && !(c.Collider2 != null && c.Collider2.Object.Controller is WormControllerWalk);
        }


        public override void OnCollision (Collision c) {
//            if (c.Collider2 != null && c.Collider2.Object.Controller is WormControllerFall) {
//                Object.Controller = new WormControllerFall();
//                Debug.LogWarning("1");
//                return;
//            }
            if (c.Collider1 == ((Worm) Object).Tail) {
                Object.Controller = new WormControllerWalk();
            }
        }

    }

}
