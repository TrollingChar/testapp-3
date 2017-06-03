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

        public override string ToString () {
            return base.ToString();
        }

        static public Collision operator - (Collision c) {
            return new Collision(
                -c.offset,
                -c.normal,
                c.collider2,
                c.collider1,
                c.primitive2,
                c.primitive1);
        }

        public bool Equals (Collision other) {
            if ((object)other == null) return false;
            return offset.sqrLength.Equals(other.offset.sqrLength);
        }

        public int CompareTo (Collision other) {
            if ((object)other == null) return 1;
            return offset.sqrLength.CompareTo(other.offset.sqrLength);
        }

        static public bool operator == (Collision a, Collision b) {
            if ((object)a == null) return (object)b == null;
            return a.Equals(b);
        }

        static public bool operator != (Collision a, Collision b) {
            if ((object)a == null) return (object)b != null;
            return !a.Equals(b);
        }

        static public bool operator < (Collision a, Collision b) {
            if ((object)a == null) return false;
            if ((object)b == null) return true;
            return a.offset.sqrLength < b.offset.sqrLength;
        }
        static public bool operator > (Collision a, Collision b) {
            return b < a;
        }
        static public bool operator <= (Collision a, Collision b) {
            return !(b < a);
        }
        static public bool operator >= (Collision a, Collision b) {
            return !(a < b);
        }
    }
}
