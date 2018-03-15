using System;
using Core;


namespace Battle.Objects.Controllers {

    [Obsolete]
    public class DisappearingController : Controller {

        public DisappearingController (Time t) {
            Timer = t;
        }


        public override void OnTimer () {
            Object.Remove();
        }

    }

}
