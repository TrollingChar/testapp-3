using Utils.Singleton;


namespace War.Objects.Controllers {

    public class Controller : Component {

        protected readonly BF BF = The<BF>.Get();

        public virtual void Update (TurnData td) {}


        protected void Wait (int milliseconds = 500) {
            BF.State.Wait(milliseconds);
        }

    }

}
