using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class BazookaShell : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new StandardController {
                SmokeSize = 20,
                OrientationFlag = true
            };
            Timer = new DetonationTimer(new Time{Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
