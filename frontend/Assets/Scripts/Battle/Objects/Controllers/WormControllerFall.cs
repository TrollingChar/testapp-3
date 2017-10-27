﻿using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerFall : StandardController {

        private float _control = 0;


        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
        }


        protected virtual void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            Wait();

            if (Object.Velocity.SqrLength > 0.5f) {
                _control = 0;
                return;
            }
            _control += 0.2f;
            if (_control >= 1f && ((Worm) Object).CanLandThere) {
                ((Worm) Object).LandThere();
            }

        }

    }

}
