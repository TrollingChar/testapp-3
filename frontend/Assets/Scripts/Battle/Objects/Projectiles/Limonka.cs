using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Limonka : Object {

        private readonly int _timer;


        public Limonka (int timer) {
            _timer = timer;
        }


        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.Limonka, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new ClusterSpawner();
            Controller = new GrenadeController(_timer * 1000);
            CollisionHandler = new CollisionHandler();
        }

    }

}
