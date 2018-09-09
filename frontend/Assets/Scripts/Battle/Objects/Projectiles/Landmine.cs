using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Landmine : Object {

        public const float Radius = 5f;
        public const float StickCheckRadius = 7f;
        public const int ActivationRadius = 40;

        
        public override void OnSpawn () {
            var transform = GameObject.transform;
            var assets = The.BattleAssets;

            UnityEngine.Object.Instantiate(assets.Landmine, transform, false);
            AddCollider(new CircleCollider(XY.Zero, Radius));
            Explosive = new Explosive25();
            Controller = new LandmineCtrl();
            Timer = new LandmineInitialTimer();
            CollisionHandler = new LandmineStickCH();
        }


        public override void ReceiveBlastWave (XY impulse) {
            Controller = new LandmineCtrl();
            CollisionHandler = new CollisionHandler();
            base.ReceiveBlastWave(impulse);
        }


        public bool CheckWormsPresence (float radius) {
            float sqrDist;
            World.WormNearestTo(Position, out sqrDist);
            return sqrDist <= radius * radius;
        }


        protected override bool PassableFor (Object o) {
            return this == o;
        }

    }

}
