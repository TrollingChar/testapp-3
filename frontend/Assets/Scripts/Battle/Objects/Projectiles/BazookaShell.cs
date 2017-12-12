using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class BazookaShell : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive40();
            Controller = new ShellController();
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
