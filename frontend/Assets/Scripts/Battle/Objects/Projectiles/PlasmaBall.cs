using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class PlasmaBall : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.PlasmaBall, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new StandardController(); // todo: wind affection
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
