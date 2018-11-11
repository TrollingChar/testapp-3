using Core;


namespace Battle.Experimental {

    public partial class BattleScene {

        private NewGameState _state;


        public NewGameState State {
            get { return _state; }
            private set {
                _state = value;
                Hint.text = value.ToString ();
                _state.Init ();
            }
        }


        private void UpdateGame () {
            var next = State.Next ();
            while (next != null) {
                State = next;
                next  = State.Next ();
            }
            State.Update ();
        }

    }

}