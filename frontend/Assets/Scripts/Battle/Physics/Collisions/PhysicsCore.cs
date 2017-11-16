using Battle.Objects;
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
            if (ab && a.Object is Worm && b.Object is Worm) {
                Debug.LogWarning(a.Object.Controller.GetType() + " >< " + b.Object.Controller.GetType());
            }
            return ab;
        }


        public static Collision FlyInto (Collider a, Collider b, XY v) {
            bool overlapped = Overlap(a, b);
            
            var ab = a.FlyInto(b, v);
            var ba = b.FlyInto(a, -v);
//            Debug.Assert(
//                ab == -ba,
//                "2nd law of collisions does not work when:\n" +
//                "a = " + a + ",\n" +
//                "b = " + b + ",\n" +
//                "v = " + v.ToString("R")
//            );

            if (overlapped) return ab;
            
            var before = a.Object.Position;

            if (a is CircleCollider && b is CircleCollider) {
                var lo = XY.Zero;
                var hi = ab == null ? v : ab.Offset;

                a.Object.Position += hi;
                if (!Overlap(a, b)) goto @return;
                
                for (int i = 0; i < 10; i++) {
                    var mid = (lo + hi) * 0.5f;
                    a.Object.Position = before + mid;
                    if (Overlap(a, b)) {
                        hi = mid;
                    } else {
                        lo = mid;
                    }
                }
                if (ab != null) ab.Offset = lo; // possible bug caused by condition
            }

            a.Object.Position = before + (ab == null ? v : ab.Offset);
            Debug.Assert(
                !Overlap(a, b),
                "3rd law of collisions does not work when:\n" +
                "a = " + a + ",\n" +
                "b = " + b + ",\n" +
                "v = " + v.ToString("R")
            );
        @return:
            a.Object.Position = before;
            return ab;
        }

    }

}
