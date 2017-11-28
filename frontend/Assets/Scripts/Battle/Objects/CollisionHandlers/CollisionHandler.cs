using Battle.Physics.Collisions;
using Collisions;


namespace Battle.Objects.CollisionHandlers {

    public class CollisionHandler : Component {

        public virtual bool WillCauseCollision (NewCollision c) {
            return true;
        }


        public virtual void OnCollision (NewCollision c) {}

    }

}
