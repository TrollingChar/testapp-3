using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;

namespace Battle.Objects.Projectiles {

    public class Grenade : Object {
        private readonly int _timer;

        public Grenade(int timer)
        {
            _timer = timer;
        }
        
        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().Grenade, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new GrenadeController(_timer * 1000); // todo: grenade controller
            CollisionHandler = new CollisionHandler();
        }
        
    }
}
