using System.Linq;
using Battle.Objects;
using Battle.Objects.Controllers;
using Core;


namespace Battle.Experimental {

    public class RemovalState : NewGameState {

        private NewGameState _state;
        private readonly BattleScene _battle = The.Battle;


        public override void Init () {
            // debug
//            The.Battle.TweenTimer.Wait ();
            
            var worms = The.World.Objects.OfType <Worm> ().Where (w => w.HP <= 0 && !w.Despawned).ToList ();
            if (worms.Count == 0) {
                _state = new BeforeTurnState ();
            }
            else {
                foreach (var worm in worms) {
                    worm.Controller = new WormDeathCtrl ();
                }
                _battle.TweenTimer.Wait ();
                _state = this;
            }
        }


        public override NewGameState Next () {
            return _battle.TweenTimer.Elapsed ? _state : null;
        }


        public override void Update () {
            _battle.UpdateWorld ();
        }

    }


}