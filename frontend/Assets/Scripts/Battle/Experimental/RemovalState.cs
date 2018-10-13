using System.Linq;
using Battle.Objects;
using Battle.Objects.Controllers;
using Core;


namespace Battle.Experimental {

    public class RemovalState : NewGameState {

        private NewGameState _state;


        public override void Init () {
            var worms = The.World.Objects.OfType <Worm> ().Where (w => w.HP <= 0 && !w.Despawned).ToList ();
            if (worms.Count == 0) {
                _state = new BeforeTurnState ();
                return;
            }
            foreach (var worm in worms) {
                worm.Controller = new WormDeathCtrl ();
            }
            _state = this;
        }


        public override NewGameState Next () {
            return The.Battle.TweenTimer.Elapsed ? _state : null;
        }


        public override void Update () {
            The.Battle.UpdateWorld ();
        }

    }


}