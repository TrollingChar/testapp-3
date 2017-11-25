using System;
using Collider = Battle.Physics.Collisions.Collider;

namespace Geometry {
    public struct Collision : IComparable<Collision> {

        public XY Offset;
        public XY Normal;
        public Collider Collider1;
        public Collider Collider2;
        
        
        public static readonly Collision Infinity
            = new Collision(new XY(float.PositiveInfinity, float.PositiveInfinity), XY.NaN, null, null);


        public Collision (XY offset, XY normal, Collider c1, Collider c2) {
            Offset = offset;
            Normal = normal;
            Collider1 = c1;
            Collider2 = c2;
        }


        public bool IsEmpty {
            get { return Normal.IsNaN; }
        }


        public static Collision operator - (Collision c) {
            return new Collision(-c.Offset, -c.Normal, c.Collider2, c.Collider1);
        }

        
        public int CompareTo(Collision other) {
            float f = Offset.SqrLength - other.Offset.SqrLength;
            return f < 0 ? -1 : f > 0 ? 1 : 0; // Math.Sign casts to double
        }


        public static bool operator < (Collision a, Collision b) {
            return a.Offset.SqrLength < b.Offset.SqrLength;
        }


        public static bool operator > (Collision a, Collision b) {
            return a.Offset.SqrLength > b.Offset.SqrLength;
        }


        public static bool operator <= (Collision a, Collision b) {
            return a.Offset.SqrLength <= b.Offset.SqrLength;
        }


        public static bool operator >= (Collision a, Collision b) {
            return a.Offset.SqrLength >= b.Offset.SqrLength;
        }
    }
}