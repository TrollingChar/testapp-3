using Core;


namespace Battle.Experimental {

    public class SynchronizingState : NewGameState {

        public override void Init () {}


        public override NewGameState Next () {
            return The.Battle.Synchronized ? new ReadyState () : null;
        }


        public override void Update () {}

    }

}