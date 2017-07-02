using War.Objects;


namespace War.Controllers {

    public class Controller {

        public Object Obj;

        public virtual void Update () {}
        public virtual void OnRemove () {}
        public virtual void OnAdd () {}


        protected void Wait (int milliseconds = 500) {
            Core.BF.State.Wait(milliseconds);
        }

    }

}
