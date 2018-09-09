using Battle.Objects.Other.Crates;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class CrateFallCtrl : StandardCtrl {

        private Time _control;
        private Crate _crate;


        public override void OnAdd () {
            _crate = (Crate) Object;
        }


        public CrateFallCtrl () {
            WaitFlag = true;
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);

            if (Object.Velocity.SqrLength < 1) _control.Ticks++;

            if (The.World.Time.Ticks % Time.TPS == 0) {
                if (_control.Seconds >= 0.9f) Object.Controller = new CrateStandCtrl();
                _control.Ticks = 0;
            }

            _crate.CheckIfCollected();
        }

    }

}
