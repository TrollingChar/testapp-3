using Battle.Objects.Controllers;
using Collisions;
using Geometry;


namespace Battle.Objects.CollisionHandlers {

    public class LandmineStickCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            Object.Velocity = XY.Zero;
            Object.Controller = new LandmineControllerStuck();
        }

    }

}
