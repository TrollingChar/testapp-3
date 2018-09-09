using Battle.Objects.Controllers;
using UnityEngine;
using Collision = Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class WormJumpCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            if (Mathf.Abs (c.Normal.Rotated90CW ().Angle) <= 1.5f) {
                Object.Controller = new WormAfterJumpCtrl ();
            }
        }

    }

}