using System;

namespace W3 {
    public class Controller {
        public Object obj;
        public virtual void Update () { }

        public virtual void OnRemove () { }

        public virtual void OnAdd () { }

        protected void Wait (int milliseconds = 500) {
            Core.bf.state.Wait(milliseconds);
        }
    }
}