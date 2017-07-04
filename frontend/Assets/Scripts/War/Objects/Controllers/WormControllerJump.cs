using War.Objects.CollisionHandlers;


namespace War.Objects.Controllers {

    internal class WormControllerJump : StandardController {

        public override void OnAdd () {
            Object.CollisionHandler = new CollisionHandlerWormJump();
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
