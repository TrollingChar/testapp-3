using Battle.Physics.Collisions;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class DetonatorCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (NewCollision c) {
            return false;
        }


        public override void OnCollision (NewCollision c) {
            Object.Explosive.Detonate();
        }

    }

}
