using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class HomingMissile : Object {

        private readonly XY _target;
        private GameObject _pointer;


        public HomingMissile (XY target) {
            _target = target;
        }

        
        public override void OnSpawn () {
            var assets = The.BattleAssets;
            UnityEngine.Object.Instantiate(assets.HomingMissile, GameObject.transform, false);
            _pointer = UnityEngine.Object.Instantiate(
                assets.PointCrosshair,
                new Vector3(_target.X, _target.Y, 0),
                Quaternion.identity
            );
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new StandardController {
                MagnetCoeff = 1,
                OrientationFlag = true,
                WaitFlag = true
            };
            Timer = new CallbackTimer(
                new Time{Seconds = 0.5f},
                () => Controller = new HomingController(_target)
            );
            CollisionHandler = new DetonatorCollisionHandler();
        }


        public void RemovePointer () {
            if (_pointer == null) return;
            UnityEngine.Object.Destroy(_pointer);
            _pointer = null;
        }


        public override void OnDespawn () {
            RemovePointer();
        }

    }

}
