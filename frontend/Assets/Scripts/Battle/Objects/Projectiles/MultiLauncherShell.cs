using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class MultiLauncherShell : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.MultiLauncherShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive15();
            Controller = new ShellController();
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
