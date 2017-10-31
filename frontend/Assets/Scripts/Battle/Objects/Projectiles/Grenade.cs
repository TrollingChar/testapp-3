using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class Grenade : Object {

        private readonly int _timer;


        public Grenade (int timer) {
            _timer = timer;
        }


        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.Grenade, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new GrenadeController(_timer * 1000); // todo: grenade controller
            CollisionHandler = new CollisionHandler();
        }

    }

}
