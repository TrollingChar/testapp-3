namespace War.Objects.Controllers {

    public class Controller : Component {

        public virtual void Update (TurnData td) {}


        protected void Wait (int milliseconds = 500) {
            Core.BF.State.Wait(milliseconds);
        }

    }

}
