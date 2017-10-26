using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;

namespace Battle.Objects.Projectiles
{
    public class Limonka : Object
    {
        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new ClusterSpawner();
            Controller = new ShellController(); // todo: grenade controller
//            CollisionHandler = new CollisionHandler();
            CollisionHandler = new DetonatorCollisionHandler();
        }
    }
}