using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.Objects.Controllers {

    public class StandardController : Controller {

        private readonly float _gravity = The<World>.Get().Gravity;


        public override void Update (TurnData td) {
            Object.Velocity.Y += _gravity;
        }

    }

}
