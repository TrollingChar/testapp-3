using Geometry;
using UnityEngine;


namespace Battle.Physics.Collisions {

    public static class PhysicsCore {

        // todo: ensure that colliders methods are not calles outside of here


        public static bool Overlap (Collider a, Collider b) {
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
        public static Collision FlyInto (Collider a, Collider b, XY v) {
//            if (Overlap(a, b)) return null;
            
            var ab = a.FlyInto(b, v);
            var ba = b.FlyInto(a, -v);
            Debug.Assert(
                ab == -ba,
                "2nd law of collisions does not work when:\n" +
                "a = " + a + ",\n" +
                "b = " + b + ",\n" +
                "v = " + v.ToString("R")
            );

            var before = a.Object.Position;
            a.Object.Position += ab == null ? v : ab.Offset;
            /*Debug.Assert(
                !Overlap(a, b),
                "3rd law of collisions does not work when:\n" +
                "a = " + a + ",\n" +
                "b = " + b + ",\n" +
                "v = " + v.ToString("R")
            );*/
            a.Object.Position = before;
            
            return ab;
        }

    }

}
