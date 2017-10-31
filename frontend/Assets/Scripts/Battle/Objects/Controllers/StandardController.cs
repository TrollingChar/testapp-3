using Core;
using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    public class StandardController : Controller {

        private readonly float _gravity = The.World.Gravity;


        protected override void DoUpdate (TurnData td) {
            Object.Velocity.Y += _gravity;
        }

    }

}
