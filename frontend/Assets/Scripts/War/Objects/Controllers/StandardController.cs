﻿namespace War.Objects.Controllers {

    internal class StandardController : Controller {

        public override void Update (TurnData td) {
            Object.Velocity.Y += Core.BF.World.Gravity;
        }

    }

}