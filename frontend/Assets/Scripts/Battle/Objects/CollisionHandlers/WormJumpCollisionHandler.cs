using Battle.Objects.Controllers;
using Collisions;
using UnityEngine;
using Collision = Battle.Physics.Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (NewCollision c) {
            // wtf
            return c.Collider1 != ((Worm) Object).Tail
                && !(c.Collider2 != null && c.Collider2.Object.Controller is WormControllerWalk);
        }


        public override void OnCollision (NewCollision c) {
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
