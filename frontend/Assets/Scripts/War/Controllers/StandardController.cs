namespace W3 {

    internal class StandardController : Controller {

        public override void Update () {
            obj.velocity.y += Core.bf.world.gravity;
        }

    }

}
