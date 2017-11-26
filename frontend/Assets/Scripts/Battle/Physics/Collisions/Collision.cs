﻿using System;
using Geometry;


namespace Battle.Physics.Collisions {

    public class Collision : IEquatable<Collision>, IComparable<Collision> {

        /*
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Collision) obj);
        }

        public override int GetHashCode()
        {
            unchecked {
                int hashCode = (Collider1 != null ? Collider1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Collider2 != null ? Collider2.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Offset.GetHashCode();
                hashCode = (hashCode * 397) ^ Normal.GetHashCode();
                hashCode = (hashCode * 397) ^ (Primitive1 != null ? Primitive1.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Primitive2 != null ? Primitive2.GetHashCode() : 0);
                return hashCode;
            }
        }
        */

        public Collider Collider1, Collider2;

        public XY Offset, Normal;


        public Collision (XY offset, XY normal, Collider c1, Collider c2) {
            Offset = offset;
            Normal = normal;
            Collider1 = c1;
            Collider2 = c2;
        }


        public int CompareTo (Collision other) {
            return ReferenceEquals(other, null) ? 1 : Offset.SqrLength.CompareTo(other.Offset.SqrLength);
        }


        public bool Equals (Collision other) {
            return !ReferenceEquals(other, null)
                && other.Offset == Offset
                && other.Normal == Normal
                && other.Collider1 == Collider1
                && other.Collider2 == Collider2;
        }


        public static Collision operator - (Collision c) {
            return ReferenceEquals(c, null) ? null : new Collision(
                -c.Offset,
                -c.Normal,
                c.Collider2,
                c.Collider1
            );
        }


        public static bool operator == (Collision a, Collision b) {
            return ReferenceEquals(a, null)
                 ? ReferenceEquals(b, null)
                 : a.Equals(b);
        }


        public static bool operator != (Collision a, Collision b) {
            return !(a == b);
        }


        public static bool operator < (Collision a, Collision b) {
            if (ReferenceEquals(a, null)) return false;
            if (ReferenceEquals(b, null)) return true;
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
