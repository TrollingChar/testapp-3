using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class DetonatorCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            return false;
        }


        public override void OnCollision (Collision c) {
            Object.Explosive.Detonate();
        }

    }

}
