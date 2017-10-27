using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerJump : StandardController {

        public override void OnAdd () {
            Object.CollisionHandler = new WormJumpCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            Wait();
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
