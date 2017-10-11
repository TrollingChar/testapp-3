using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {
    public class WormControllerJump : StandardController {

        public override void OnAdd () {
            Object.CollisionHandler = new WormJumpCollisionHandler();
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
