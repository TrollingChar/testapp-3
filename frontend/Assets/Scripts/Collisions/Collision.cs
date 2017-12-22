﻿using System;
using Geometry;


namespace Collisions {

    public class Collision : IComparable<Collision> {

        public XY Offset;
        public XY Normal;
        public Collider Collider1, Collider2;
        public Primitive Primitive1, Primitive2;


        public Collision (XY offset, Primitive p1, Primitive p2) {
            Offset = offset;
            Primitive1 = p1;
            Primitive2 = p2;
        }


        public Collision (XY offset, XY normal, Collider c1, Collider c2, Primitive p1, Primitive p2) {
            Offset = offset;
            Normal = normal;
            Collider1 = c1;
            Collider2 = c2;
            Primitive1 = p1;
            Primitive2 = p2;
        }


        public void ImprovePrecision () {
            PrimitiveUtils.GetOffsetNormal(Primitive1, Primitive2, ref Offset, out Normal);
        }


        public int CompareTo (Collision other) {
            return other == null ? -1 : Offset.SqrLength.CompareTo(other.Offset.SqrLength);
        }


        public bool IsLandCollision {
            get { return Collider2 == null; }
        }


        public static Collision operator - (Collision c) {
            return c == null ? null
                : new Collision(-c.Offset, -c.Normal, c.Collider2, c.Collider1, c.Primitive2, c.Primitive1);
        }


        public Collision Inverse () {
            // возможно буду использовать этот метод чтобы не нагружать GC
            Offset = -Offset;
            Normal = -Normal;
            var c = Collider1;
            Collider1 = Collider2;
            Collider2 = c;
            var p = Primitive1;
            Primitive1 = Primitive2;
            Primitive2 = p;
            return this;
        }


        public static bool operator < (Collision a, Collision b) {
            if (a == null) return false;
            if (b == null) return true;
            return a.Offset.SqrLength < b.Offset.SqrLength;
        }


        public static bool operator > (Collision a, Collision b) {
            if (b == null) return false;
            if (a == null) return true;
            return a.Offset.SqrLength > b.Offset.SqrLength;
        }


        public static bool operator <= (Collision a, Collision b) {
            if (b == null) return true;
            if (a == null) return false;
            return a.Offset.SqrLength <= b.Offset.SqrLength;
        }


        public static bool operator >= (Collision a, Collision b) {
            if (a == null) return true;
            if (b == null) return false;
            return a.Offset.SqrLength >= b.Offset.SqrLength;
        }


        public override string ToString () {
            return
                "[ collision\n" +
                "  [ offset = " + Offset.ToString("R") + " ]\n" +
                "  [ normal = " + Normal.ToString("R") + " ]\n" +
                "  [ collider 1 = " + (Collider1 == null ? "null" : Collider1.ToString()) + " ]\n" +
                "  [ collider 2 = " + (Collider2 == null ? "null" : Collider2.ToString()) + " ]\n" +
                "  [ primitive 1 = " + Primitive1 + " ]\n" +
                "  [ primitive 2 = " + Primitive2 + " ]\n" +
                "]";
        }

    }

}
