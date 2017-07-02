using System;


namespace W3 {

    public class Collision : IEquatable<Collision>, IComparable<Collision> {

        public XY offset, normal;
        public Collider collider1, collider2;
        public Primitive primitive1, primitive2;


        public Collision (
            XY offset,
            XY normal,
            Collider c1,
            Collider c2,
            Primitive p1,
            Primitive p2
        ) {
            this.offset = offset;
            this.normal = normal;
            collider1 = c1;
            collider2 = c2;
            primitive1 = p1;
            primitive2 = p2;
        }


        public static Collision operator - (Collision c) {
            return c == null
                ? null
                : new Collision(
                    -c.offset,
                    -c.normal,
                    c.collider2,
                    c.collider1,
                    c.primitive2,
                    c.primitive1
                );
        }


        public bool Equals (Collision other) {
            return (object) other != null && offset.sqrLength.Equals(other.offset.sqrLength);
        }


        public int CompareTo (Collision other) {
            return (object) other == null ? 1 : offset.sqrLength.CompareTo(other.offset.sqrLength);
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
            return a.offset.sqrLength < b.offset.sqrLength;
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
