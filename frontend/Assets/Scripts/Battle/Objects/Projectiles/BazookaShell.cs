using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Geometry;

namespace Battle.Objects.Projectiles
{
    public class BazookaShell : Object
    {
        public override void OnAdd()
        {
//            UnityEngine.Object.Instantiate()
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
        }
    }
}