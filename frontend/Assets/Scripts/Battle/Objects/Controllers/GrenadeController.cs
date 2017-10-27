using DataTransfer.Data;

namespace Battle.Objects.Controllers
{
    public class GrenadeController : StandardController
    {
        public GrenadeController(int timer) {
            Timer = timer;
        }

        protected override void DoUpdate(TurnData td)
        {
            base.DoUpdate(td);
            Wait();
        }

        public override void OnTimer() {
            Object.Detonate();
        }
    }
}