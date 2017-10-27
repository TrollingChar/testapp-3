using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.Objects.Controllers {

    public class StandardController : Controller {

        private readonly float _gravity = The<World>.Get().Gravity;


        protected override void DoUpdate (TurnData td) {
            Object.Velocity.Y += _gravity;
        }

    }

}
