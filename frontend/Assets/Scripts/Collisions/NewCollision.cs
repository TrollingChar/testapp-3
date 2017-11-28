using System;
using Battle.Physics.Collisions;
using Geometry;
using Primitive = Geometry.Primitive;


namespace Collisions {

    public class NewCollision : IComparable<NewCollision> {

        public XY Offset;
        public XY Normal;
        public Collider Collider1, Collider2;
        public Primitive Primitive1, Primitive2;


        public NewCollision (XY offset, Primitive p1, Primitive p2) {
            Offset = offset;
            Primitive1 = p1;
            Primitive2 = p2;
        }


        public NewCollision (XY offset, XY normal, Collider c1, Collider c2, Primitive p1, Primitive p2) {
            Offset = offset;
            Normal = normal;
            Collider1 = c1;
            Collider2 = c2;
            Primitive1 = p1;
            Primitive2 = p2;
        }


        public void ImprovePrecision () {
            throw new NotImplementedException();
        }

        
        public int CompareTo(NewCollision other) {
            return other == null ? -1 : Offset.SqrLength.CompareTo(other.Offset.SqrLength);
        }
        
        
        // null > everything


        public static NewCollision operator - (NewCollision c) {
            return new NewCollision(-c.Offset, -c.Normal, c.Collider2, c.Collider1, c.Primitive2, c.Primitive1);
        }


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
