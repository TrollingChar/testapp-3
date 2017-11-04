using Geometry;
using UnityEngine;

namespace Battle.Physics.Collisions
{
    public static class PhysicsCore
    {
        // todo: ensure that colliders methods are not calles outside of here

        public static bool Overlap(Collider a, Collider b)
        {
            bool ab = a.Overlaps(b);
            bool ba = b.Overlaps(a);
            Debug.Assert(
                ab == ba,
                "1st law of collisions does not work when:\n" +
                "a = " + a + ",\n" +
                "b = " + b + "!"
            );
            return ab;
        }

        // todo: 3rd law
        public static Collision FlyInto(Collider a, Collider b, XY v)
        {
            Collision ab = a.FlyInto(b, v);
            Collision ba = b.FlyInto(a, -v);
            Debug.Assert(
                ab == -ba,
                "2nd law of collisions does not work when:\n" +
                "a = " + a + ",\n" +
                "b = " + b + ",\n" +
                "v = " + v.ToString("R")
            );
            return ab;
        }
    }
}