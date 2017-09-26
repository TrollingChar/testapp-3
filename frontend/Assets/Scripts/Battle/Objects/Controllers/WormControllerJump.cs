using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    internal class WormControllerJump : StandardController {

        public override void OnAdd () {
            Object.CollisionHandler = new CollisionHandlerWormJump();
        }


        public override void Update (TurnData td) {
            base.Update(td);
            Wait();
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
