using Geometry;


namespace War.Objects.Controllers {

    internal class WormControllerWalk : Controller {

        public override void OnAdd () {
            Object.Velocity = XY.Zero;
        }


        public override void Update (TurnData td) {

            if (td == null || !td.W || !(td.A ^ td.D)) return;

            Object.Velocity = new XY(td.A ? -4 : 4, 4);
            Object.Controller = new WormControllerJump();
        }

    }

}
