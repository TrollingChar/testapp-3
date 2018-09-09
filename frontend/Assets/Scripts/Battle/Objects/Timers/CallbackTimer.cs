using System;
using Core;


namespace Battle.Objects.Timers {

    public class CallbackTimer : Timer {

        private readonly Action _callback;


        public CallbackTimer (Time time, Action callback) : base(time) {
            _callback = callback;
        }


        protected override void OnExpire () {
            _callback();
        }

    }

}
