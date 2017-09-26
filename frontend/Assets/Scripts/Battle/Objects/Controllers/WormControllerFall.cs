using DataTransfer.Data;


namespace Battle.Objects.Controllers {

    internal class WormControllerFall : StandardController {

        private int _stillTime = 0;


        public override void Update (TurnData td) {
            base.Update(td);
            Wait();
        }

    }

}
