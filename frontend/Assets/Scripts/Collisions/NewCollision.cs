using System;
using Battle.Physics.Collisions;
using Geometry;
using Primitive = Geometry.Primitive;


namespace Collisions {

    public class NewCollision : IComparable<NewCollision> {

        public XY Offset;
        public XY Normal;
        public Collider C1, C2;
        public Primitive P1, P2;


        public NewCollision (XY offset, Primitive p1, Primitive p2) {
            Offset = offset;
            P1 = p1;
            P2 = p2;
        }


        public void ImprovePrecision () {
            throw new NotImplementedException();
        }

        
        public int CompareTo(NewCollision other) {
            return other == null ? 1 : Offset.SqrLength.CompareTo(other.Offset.SqrLength);
        }
        
        
        // null > everything


        public static bool operator < (NewCollision a, NewCollision b) {
            if (a == null) return false;
            if (b == null) return true;
            return a.Offset.SqrLength < b.Offset.SqrLength;
        }


        public static bool operator > (NewCollision a, NewCollision b) {
            if (b == null) return false;
            if (a == null) return true;
            return a.Offset.SqrLength > b.Offset.SqrLength;
        }


        public static bool operator <= (NewCollision a, NewCollision b) {
            if (b == null) return true;
            if (a == null) return false;
            return a.Offset.SqrLength <= b.Offset.SqrLength;
        }


        public static bool operator >= (NewCollision a, NewCollision b) {
            if (a == null) return true;
            if (b == null) return false;
            return a.Offset.SqrLength >= b.Offset.SqrLength;
        }
    }

}
