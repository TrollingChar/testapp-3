using Battle.Objects.Controllers;
using Collisions;
using Geometry;


namespace Battle.Objects.CollisionHandlers {

    public class LandmineStickCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            Object.Velocity = XY.Zero;
            var controller = (LandmineController) Object.Controller;
            controller.Stuck = true;
        }

    }

}
