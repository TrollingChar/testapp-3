using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class DetonatorCollisionHandler : CollisionHandler {

        public override void OnCollision (Collision c) {
            Object.Explosive.Detonate();
        }

    }

}
