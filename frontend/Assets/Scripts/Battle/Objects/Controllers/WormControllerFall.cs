using Battle.Objects.CollisionHandlers;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class WormControllerFall : StandardController {

        private float _control;


        public override void OnAdd () {
            Object.CollisionHandler = new WormFallCollisionHandler();
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            Wait();

            if (Object.Velocity.SqrLength > 4f) {
                _control = 0;
                return;
            }
            _control += 0.02f;
            if (_control >= 1f && ((Worm) Object).CanLandThere) {
                ((Worm) Object).LandThere();
            }

        }

    }

}
