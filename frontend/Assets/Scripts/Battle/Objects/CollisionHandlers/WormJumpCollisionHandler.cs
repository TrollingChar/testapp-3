using Battle.Objects.Controllers;
using Collisions;
using UnityEngine;
using Collision = Battle.Physics.Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (NewCollision c) {
            // wtf
            if (Mathf.Abs(c.Normal.Rotated90CW().Angle) <= 1.5f) return false;
            return c.Collider2 == null || !(c.Collider2.Object.Controller is WormControllerWalk);
        }


        public override void OnCollision (NewCollision c) {
            if (Mathf.Abs(c.Normal.Rotated90CW().Angle) <= 1.5f) Object.Controller = new WormControllerWalk();
        }

    }

}
