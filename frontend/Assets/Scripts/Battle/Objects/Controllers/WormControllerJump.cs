using Battle.Objects.CollisionHandlers;


namespace Battle.Objects.Controllers {

    internal class WormControllerJump : StandardController {

        public override void OnAdd () {
            Object.CollisionHandler = new CollisionHandlerWormJump();
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
