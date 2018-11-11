using Battle.Experimental;
using Core;


namespace Battle.State {

    public class SynchronizingState : NewGameState {

        private readonly BattleScene _battle = The.Battle;

        
        public override void Init () {}


        public override NewGameState Next () {
            return _battle.Synchronized ? new ReadyState () : null;
        }


        public override void Update () {}

    }

}