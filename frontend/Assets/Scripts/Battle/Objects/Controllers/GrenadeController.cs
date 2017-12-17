﻿using DataTransfer.Data;
using UnityEngine.UI;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class GrenadeController : StandardController {

        public Text TimerText { get; private set; }


        public GrenadeController (Time t, Text timerText) {
            TimerText = timerText;
            Timer = t;
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            if (TimerText != null) TimerText.text = Timer.ToString();
            Wait();
        }


        public override void OnTimer () {
            Object.Detonate();
        }

    }

}
