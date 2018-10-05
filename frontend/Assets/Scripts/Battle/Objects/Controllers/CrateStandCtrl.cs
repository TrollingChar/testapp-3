using Battle.Objects.Other.Crates;
using DataTransfer.Data;
using Geometry;


namespace Battle.Objects.Controllers {

    public class CrateStandCtrl : Controller {

        private Crate _crate;


        public override void OnAdd () {
            Object.Velocity = XY.Zero;
//            Object.Immobile = true;
            _crate = (Crate) Object;
        }


//        public override void OnRemove () {
//            Object.Immobile = false;
//        }


        protected override void DoUpdate (TurnData td) {
            Object.Velocity = XY.Down;
            Object.ExcludeObjects();
            if (Object.NextCollision(1f) == null) Object.Controller = new CrateFallCtrl();
            Object.Velocity = XY.Zero;
            
            _crate.CheckIfCollected();
        }

    }

}
