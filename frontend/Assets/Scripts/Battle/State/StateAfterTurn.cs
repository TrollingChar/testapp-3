using System.Linq;
using Battle.Objects;
using Core;
using UI;


namespace Battle.Experimental {

    public class AfterTurnState : NewGameState {

        private readonly BattleScene _battle = The.Battle;


        public override void Init () {
            _battle.Alert.Alpha = 0;
            var worms = _battle.World.Objects.OfType <Worm> ().Where (w => w.Poison > 0 && !w.Despawned).ToList ();
            if (worms.Count == 0) return;
            foreach (var worm in worms) {
                worm.TakeDamage (worm.Poison);
            }
            _battle.TweenTimer.Wait ();
        }


        public override NewGameState Next () {
            return _battle.TweenTimer.Elapsed ? new RemovalState () : null;
        }


        public override void Update () {
            _battle.UpdateWorld ();
        }

    }


}