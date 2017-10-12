using Battle.Physics.Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class CollisionHandler : Component {

        public virtual bool WillCauseCollision (Collision c) {
            return true;
        }


        public virtual void OnCollision (Collision c) {}

    }

}
