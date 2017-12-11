using Battle.Objects.Controllers;
using Collisions;
using UnityEngine;
using Collision = Battle.Physics.Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (NewCollision c) {
            // wtf
            var worm = (Worm) Object;
            if (c.Collider1 == worm.Tail) return false; // todo: check normal
            return c.Collider2 == null || !(c.Collider2.Object.Controller is WormControllerWalk);
        }


        public override void OnCollision (NewCollision c) {
//            if (c.Collider2 != null && c.Collider2.Object.Controller is WormControllerFall) {
//                Object.Controller = new WormControllerFall();
//                Debug.LogWarning("1");
//                return;
//            }
            var worm = (Worm) Object;
            if (c.Collider1 == worm.Tail) Object.Controller = new WormControllerWalk(); // todo: same
        }

    }

}
