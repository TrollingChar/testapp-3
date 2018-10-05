using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class VacuumBomb : Object {

        public override void OnSpawn () {
            UnityEngine.Object.Instantiate (The.BattleAssets.VacuumBomb, GameObject.transform, false);
            AddCollider (new CircleCollider (XY.Zero, 2f));
            Explosive = new Implosive40 ();
            Controller = new StandardCtrl {
                SmokeSize       = 20,
                OrientationFlag = true
            };
            Timer            = new DetonationTimer (new Time {Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler ();
        }

    }

}