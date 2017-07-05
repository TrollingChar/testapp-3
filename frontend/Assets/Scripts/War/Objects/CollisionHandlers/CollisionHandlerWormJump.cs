using War.Objects.Controllers;
using War.Physics.Collisions;


namespace War.Objects.CollisionHandlers {

    public class CollisionHandlerWormJump : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            return
                !(c.Collider2.Object.Controller is WormControllerWalk)
                && c.Collider1 != ((Worm) Object).Tail;
        }


        public override void OnCollision (Collision c) {
            if (c.Collider1 == ((Worm) Object).Tail) Object.Controller = new WormControllerWalk();
        }

    }

}
