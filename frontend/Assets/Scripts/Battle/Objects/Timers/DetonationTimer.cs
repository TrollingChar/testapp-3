using Battle.Experimental;
using Battle.State;
using Core;
using UnityEngine.UI;


namespace Battle.Objects.Timers {

    public class DetonationTimer : Timer {

        private readonly NewTimer _timer = The.Battle.TweenTimer;
        private readonly Text     _text;
        private readonly int      _limit;


        public DetonationTimer (Time time, Text text = null, int limit = 5) : base (time) {
            _text  = text;
            _limit = limit;
        }


        protected override void OnExpire () {
            Object.Detonate ();
        }


        protected override void OnTick () {
            _timer.Wait ();
            if (_text == null) return;
            _text.text = _limit == 0 ? Time.ToString () : Time.ToString (_limit);
        }

    }

}