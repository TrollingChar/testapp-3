using Collisions;
using Geometry;


namespace Battle.Objects.CollisionHandlers {

    public class DynamiteCollisionHandler : CollisionHandler {

        public override bool WillCauseCollision (Collision c) {
            return true;
        }


        public override void OnCollision (Collision c) {
            Object.Velocity.X = 0;// = new XY(0, 5f);
        }

    }

}
