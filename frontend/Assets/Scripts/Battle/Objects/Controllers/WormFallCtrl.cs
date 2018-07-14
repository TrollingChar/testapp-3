using Battle.Objects.CollisionHandlers;
using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerFall : StandardController {

        private Time _control;


        public WormControllerFall () {
            WaitFlag = true;
        }
        

        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            var worm = (Worm) Object;
            worm.Name = "fall";
//            Wait();

            if (Object.Velocity.SqrLength < 1) _control.Ticks++;
            
            if (The.World.Time.Ticks % Time.TPS != 0) return;

            if (_control.Seconds >= 0.9f) worm.Controller = new WormControllerJump();
            _control.Ticks = 0;
        }

    }

}
