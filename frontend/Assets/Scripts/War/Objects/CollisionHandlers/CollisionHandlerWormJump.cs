using War.Objects.Controllers;
using War.Physics.Collisions;


namespace War.Objects.CollisionHandlers {

    public class CollisionHandlerWormJump : CollisionHandler {

        public override void OnCollision (Collision c) {
            if (c.Collider1 == ((Worm) Object).Tail) Object.Controller = new WormControllerWalk();
        }

    }

}
