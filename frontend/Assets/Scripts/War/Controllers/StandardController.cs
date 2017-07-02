namespace War.Controllers {

    internal class StandardController : Controller {

        public override void Update () {
            Obj.Velocity.Y += Core.BF.World.Gravity;
        }

    }

}
