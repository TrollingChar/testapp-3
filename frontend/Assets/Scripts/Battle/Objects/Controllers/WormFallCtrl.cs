using Battle.Objects.CollisionHandlers;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormFallCtrl : StandardCtrl {

        private Time _control;


        public WormFallCtrl () {
            WaitFlag = true;
        }
        

        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
            ((Worm) Object).NewWormGO.Fall ();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            var worm = (Worm) Object;
            worm.Name = "fall";

            if (Object.Velocity.SqrLength < 1) _control.Ticks++;
            
            if (The.World.Time.Ticks % Time.TPS != 0) return;

            if (_control.Seconds >= 0.9f) worm.Controller = new WormRecoverCtrl();
            _control.Ticks = 0;
        }


        public override void OnRemove () {
            Object.CollisionHandler = null;
        }

    }

}
