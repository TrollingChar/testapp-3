using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormJumpCtrl : StandardCtrl {

        private Worm _worm;


        public WormJumpCtrl () {
            WaitFlag = true;
        }
        
        
        public override void OnAdd () {
            _worm = (Worm) Object;
            Object.CollisionHandler = new WormJumpCH();
            _worm.NewWormGO.Jump ();
        }


        protected override void DoUpdate (TurnData td) {
            _worm.Name = "jump";
            base.DoUpdate(td);
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
