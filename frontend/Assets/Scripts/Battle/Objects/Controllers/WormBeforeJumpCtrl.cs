using Battle.Objects.Timers;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;


namespace Battle.Objects.Controllers {

    public class WormBeforeJumpCtrl : Controller {

        private XY _v;


        public WormBeforeJumpCtrl (XY v) {
            _v = v;
        }


        public override void OnAdd () {
            Object.Immobile = true;
            Object.Timer = new CallbackTimer (
                new Time {Seconds = 0.2f},
                () => {
                    Object.Velocity   = _v;
                    Object.Controller = new WormJumpCtrl ();
                }
            );
            ((Worm) Object).NewWormGO.PrepareJump ();
        }


        protected override void DoUpdate (TurnData td) {
            var worm = (Worm) Object;

            worm.Name = "before jump";

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