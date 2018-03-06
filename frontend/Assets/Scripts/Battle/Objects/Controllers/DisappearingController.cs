using Core;


namespace Battle.Objects.Controllers {

    public class DisappearingController : Controller {

        public DisappearingController (Time t) {
            Timer = t;
        }


        public override void OnTimer () {
            Object.Remove();
        }

    }

}
