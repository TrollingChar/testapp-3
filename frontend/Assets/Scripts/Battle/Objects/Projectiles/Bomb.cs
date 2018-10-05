using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Bomb : Object {

        public override void OnSpawn () {
            UnityEngine.Object.Instantiate (The.BattleAssets.Bomb, GameObject.transform, false);
            AddCollider (new CircleCollider (XY.Zero, 2f));
            Explosive = new Explosive15 ();
            Controller = new StandardCtrl {
                SmokeSize       = 10,
                OrientationFlag = true
            };
            Timer            = new DetonationTimer (new Time {Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler ();
        }

    }

}