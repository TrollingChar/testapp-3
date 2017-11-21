using System;
using UnityEngine;
using Collider = Battle.Physics.Collisions.Collider;

namespace Geometry {
    public struct NCollision : IComparable<NCollision> {

        public readonly XY Offset;
        public readonly XY Normal;
        public readonly Collider C1;
        public readonly Collider C2;


        public NCollision (XY offset, XY normal, Collider c1, Collider c2) {
            Offset = offset;
            Normal = normal;
            C1 = c1;
            C2 = c2;
        }


        public static NCollision operator - (NCollision c) {
            return new NCollision(-c.Offset, -c.Normal, c.C2, c.C1);
        }

        
        public int CompareTo(NCollision other) {
            float f = Offset.SqrLength - other.Offset.SqrLength;
            return f < 0 ? -1 : f > 0 ? 1 : 0; // Math.Sign casts to double
        }
    }
}