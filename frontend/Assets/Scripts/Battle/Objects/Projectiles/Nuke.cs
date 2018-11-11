using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Nuke : Object {

        public override void OnSpawn () {
            var transform = GameObject.transform;
            var assets    = The.BattleAssets;

            UnityEngine.Object.Instantiate (assets.BazookaShell, transform, false);
//            AddCollider (new CircleCollider (XY.Zero,         20f));
            AddCollider (new CircleCollider (new XY (0, -50), 20f));
            Explosive        = new ExplosiveNuke ();
            Controller       = new StandardCtrl {
                SmokeSize       = 40,
                OrientationFlag = true
            };
            Timer            = new DetonationTimer (new Time {Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler ();
        }

    }

}