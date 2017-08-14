using Utils.Singleton;


namespace War.Objects.Controllers {

    internal class StandardController : Controller {

        private float _gravity = The<World>.Get().Gravity;


        public override void Update (TurnData td) {
            Object.Velocity.Y += _gravity;
        }

    }

}
