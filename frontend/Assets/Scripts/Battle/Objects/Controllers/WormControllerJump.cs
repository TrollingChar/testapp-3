using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerJump : StandardController {

        public WormControllerJump () {
            WaitFlag = true;
        }
        
        
        public override void OnAdd () {
            Object.CollisionHandler = new WormJumpCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            ((Worm) Object).Name = "jump";
            base.DoUpdate(td);
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
