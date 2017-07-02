using Geometry;


namespace War.Controllers {

    internal class WormControllerWalk : Controller {

        public override void OnAdd () {
            Obj.Velocity = XY.Zero;
        }


        public override void Update () {
            /*
            var worm = obj as Worm;
            Core.bf.world.TestWormBall(
                worm.position + new XY(0, Worm.bodyHeight * 0.5f),


            if (obj != Core.bf.state.worm) return;

            // move*/
        }

    }

}
