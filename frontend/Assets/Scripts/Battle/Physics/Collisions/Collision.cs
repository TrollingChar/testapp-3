using System;
using Geometry;


namespace Battle.Physics.Collisions {

    public class Collision : IEquatable<Collision>, IComparable<Collision> {

        public Collider Collider1, Collider2;

        public XY Offset, Normal;
        public Primitive Primitive1, Primitive2;


        public Collision (
            XY offset,
            XY normal,
            Collider c1,
            Collider c2,
            Primitive p1,
            Primitive p2
        ) {
            Offset = offset;
            Normal = normal;
            Collider1 = c1;
            Collider2 = c2;
            Primitive1 = p1;
            Primitive2 = p2;
        }


        public int CompareTo (Collision other) {
            return (object) other == null ? 1 : Offset.SqrLength.CompareTo(other.Offset.SqrLength);
        }


        public bool Equals (Collision other) {
            return (object) other != null && Offset.SqrLength.Equals(other.Offset.SqrLength);
        }


        public static Collision operator - (Collision c) {
            return c != null
                ? new Collision(
                    -c.Offset,
                    -c.Normal,
                    c.Collider2,
                    c.Collider1,
                    c.Primitive2,
                    c.Primitive1
                ) : null;
        }


        public static bool operator == (Collision a, Collision b) {
            return (object) a == null
                ? (object) b == null
                : a.Equals(b);
        }


        public static bool operator != (Collision a, Collision b) {
            return (object) a == null
                ? (object) b != null
                : !a.Equals(b);
        }


        public static bool operator < (Collision a, Collision b) {
            if ((object) a == null) return false;
            if ((object) b == null) return true;
            return a.Offset.SqrLength < b.Offset.SqrLength;
        }


        public static bool operator > (Collision a, Collision b) {
            return b < a;
        }


        public static bool operator <= (Collision a, Collision b) {
            return !(b < a);
        }


        public static bool operator >= (Collision a, Collision b) {
            return !(a < b);
        }

    }

}
