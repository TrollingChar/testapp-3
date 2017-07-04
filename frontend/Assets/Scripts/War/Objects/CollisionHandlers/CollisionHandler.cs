using War.Physics.Collisions;


namespace War.Objects.CollisionHandlers {

    public class CollisionHandler : Component {

        public virtual bool WillAcceptCollision (Collision c) { return true; }
        public virtual void OnCollision (Collision c) {}

    }

}
