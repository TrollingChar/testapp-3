using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;


namespace Battle.Objects.Projectiles {

    public class Limonka : Object {

        private readonly int _timer;


        public Limonka (int timer) {
            _timer = timer;
        }


        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().Limonka, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new ClusterSpawner();
            Controller = new GrenadeController(_timer * 1000);
            CollisionHandler = new CollisionHandler();
        }

    }

}
