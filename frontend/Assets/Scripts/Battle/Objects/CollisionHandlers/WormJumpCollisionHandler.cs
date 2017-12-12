using Battle.Objects.Controllers;
using Collisions;
using UnityEngine;
using Collision = Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            // wtf
            if (Mathf.Abs(c.Normal.Rotated90CW().Angle) <= 1.5f) return false;
            return c.Collider2 == null || !(c.Collider2.Object.Controller is WormControllerWalk);
        }


        public override void OnCollision (Collision c) {
            if (Mathf.Abs(c.Normal.Rotated90CW().Angle) <= 1.5f) Object.Controller = new WormControllerWalk();
        }

    }

}
