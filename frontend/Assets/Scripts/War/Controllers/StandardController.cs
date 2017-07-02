namespace War.Controllers {

    internal class StandardController : Controller {

        public override void Update () {
            obj.velocity.y += Core.bf.world.gravity;
        }

    }

}
