using System;
using Battle.Objects.Controllers;
using Battle.Physics.Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class WormFallCollisionHandler : CollisionHandler {
        
        // bug: 2 worms in a pit cause game to never end a turn

        public override void OnCollision (Collision c) {
            if (c.Collider2 == null) return;
            var worm = c.Collider2.Object as Worm;
            if (worm == null) return;
            worm.Controller = new WormControllerFall();
        }

    }

}