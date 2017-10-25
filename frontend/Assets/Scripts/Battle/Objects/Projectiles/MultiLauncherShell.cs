using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;


namespace Battle.Objects.Projectiles {

    public class MultiLauncherShell : Object {

        public override void OnAdd () {
            // todo: sprite
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive15();
            Controller = new ShellController();
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}