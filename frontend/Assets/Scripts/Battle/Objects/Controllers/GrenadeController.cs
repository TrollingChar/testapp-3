using DataTransfer.Data;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.Controllers {

    public class GrenadeController : StandardController {

        public Text TimerText { get; private set; }


        public GrenadeController (int timer, Text timerText) {
            TimerText = timerText;
            Timer = timer;
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            if (TimerText != null) TimerText.text = Mathf.CeilToInt(Timer / 1000f).ToString();
            Wait();
        }


        public override void OnTimer () {
            Object.Detonate();
        }

    }

}
