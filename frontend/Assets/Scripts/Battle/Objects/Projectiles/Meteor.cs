using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Meteor : Object {

        public override void OnSpawn () {
            UnityEngine.Object.Instantiate(The.BattleAssets.Meteor, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 20f));
            Explosive = new Explosive40();
            Controller = new StandardController {
                SmokeSize = 40,
                OrientationFlag = true
            };
            Timer = new DetonationTimer(new Time{Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler();
        }

        

    }

}
