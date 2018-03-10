using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class HomingMissile : Object {

        private readonly XY _target;


        public HomingMissile (XY target) {
            _target = target;
        }

        
        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new StandardController {
                MagnetCoeff = 1,
                OrientationFlag = true,
                WaitFlag = true
            };
//            Timer = new DetonationTimer(new Time{Seconds = 20}); todo
            Timer = new CallbackTimer(new Time{Seconds = 0.5f}, () => Controller = new HomingController(_target));
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
