using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class StandardController : Controller {

        protected override void DoUpdate (TurnData td) {
            Object.Velocity.Y += The.World.Gravity;
            Wait();
        }

    }

}
