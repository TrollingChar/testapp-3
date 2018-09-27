using Battle.Objects.Other.Crates;
using Battle.State;
using Core;
using Collision = Collisions.Collision;


namespace Battle.Objects.CollisionHandlers {

    public class CrateCH : CollisionHandler {

        public override void OnCollision (Collision c) {
            if (c.IsLandCollision) return;
            
            var worm = c.Collider2.Object as Worm;
            var battle = The.BattleScene;
            
            if (battle.State.CurrentState == GameState.Turn && battle.ActiveWorm.Is(worm)) {
                ((Crate) Object).CollectBy(worm);
            }
        }

    }

}
