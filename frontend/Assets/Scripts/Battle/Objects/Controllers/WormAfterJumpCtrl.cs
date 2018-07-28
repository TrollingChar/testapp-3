using Battle.Objects.Timers;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;


namespace Battle.Objects.Controllers {

    public class WormAfterJumpCtrl : Controller {

        public override void OnAdd () {
            Object.Velocity = XY.Zero;
            Object.Immobile = true;
            Object.Timer = new CallbackTimer (
                new Time {Seconds = 0.2f},
                () => { Object.Controller = new WormWalkCtrl (); }
            );
            ((Worm) Object).NewWormGO.Land ();
        }


        protected override void DoUpdate (TurnData td) {
            var worm = (Worm) Object;

            worm.Name = "after jump";

            var collision = new Ray(worm.Tail.Center, new CircleCollider(XY.Zero, Worm.HeadRadius)) {Object = Object}
            .Cast(new XY(0f, -Worm.MaxDescend));

            // will fall?
            if (collision == null) {
                Object.Controller = new WormJumpCtrl();
                return;
            }
            
            Wait();
        }


        public override void OnRemove () {
            Object.Timer = null;
        }

        

    }

}