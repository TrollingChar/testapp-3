using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;
using Utils.Singleton;

namespace Battle.Objects.Projectiles
{
    public class BazookaShell : Object
    {
        public override void OnAdd()
        {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().BazookaShell, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new BazookaShellController();
            CollisionHandler = new DetonatorCollisionHandler();
        }
    }
}