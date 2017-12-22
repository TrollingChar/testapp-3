using Battle.Objects.CollisionHandlers;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerFall : StandardController {

        private Time _control;


        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            ((Worm) Object).Name = "fall";
            Wait();

            if (Object.Velocity.SqrLength < 1) _control.Ticks++;
            
            if (The.World.Time.Ticks % Time.TPS != 0) return;
            
            var worm = (Worm) Object;
            if (_control.Seconds >= 0.9f && worm.CanLandThere) worm.LandThere();
            _control.Ticks = 0;
        }

    }

}
