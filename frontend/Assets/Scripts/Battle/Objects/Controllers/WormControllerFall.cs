using Battle.Objects.CollisionHandlers;
using Core;
using DataTransfer.Data;
using UnityEngine;


namespace Battle.Objects.Controllers {

    public class WormControllerFall : StandardController {

        private int _control;


        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            ((Worm) Object).Name = "fall";
            Wait();

            if (Object.Velocity.SqrLength < 1) _control += 20;
            if (The.World.Time % 1000 == 0) {
                // 50 ticks
                var worm = (Worm) Object;
                if (_control >= 900 && worm.CanLandThere) {
                    worm.LandThere();
                }
                _control = 0;
            }

        }

    }

}
