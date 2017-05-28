using System;

namespace W3 {
    public class Collision {
        public Collider collider1, collider2;
        public XY offset, normal;
        public Primitive primitive1, primitive2;

        public Collision () {

        }

        public override string ToString () {
            return base.ToString();
        }

        static public Collision operator - (Collision c) {
            return null;
        }

        static public bool operator == (Collision a, Collision b) {
            throw new NotImplementedException();
        }
        static public bool operator != (Collision a, Collision b) {
            throw new NotImplementedException();
        }
        static public bool operator < (Collision a, Collision b) {
            if (a == null) return false;
            if (b == null) return true;
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
