using Collisions;
using Geometry;


namespace Battle.Objects.CollisionHandlers {

    public class DynamiteCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            Object.Velocity.X = 0;// = new XY(0, 5f);
        }

    }

}
