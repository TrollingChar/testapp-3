using Battle.Camera;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class LimonkaCluster : Object {

        public override void OnSpawn () {
            UnityEngine.Object.Instantiate (The.BattleAssets.LimonkaCluster, GameObject.transform, false);
            AddCollider (new CircleCollider (XY.Zero, 2f));
            Explosive = new Explosive10Wide ();
            Controller = new StandardCtrl {
//                SmokeSize = 10
            };
            Timer            = new DetonationTimer (new Time {Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler ();
        }

    }

}