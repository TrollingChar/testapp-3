using System;
using Geometry;


namespace Battle.Physics.Collisions {

    [Obsolete]
    public class CirclePrimitive : Primitive {

        public XY Center;
        public float Radius;


        private CirclePrimitive (XY o, float r) {
            Center = o;
            Radius = r;
        }


        public static CirclePrimitive New (XY center, float radius = 0) {
            return new CirclePrimitive(center, radius);
        }


        public override float Locate (Primitive other, XY offset) {
            return other.LocateCircle(this, -offset);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (Center - other.Center).SqrLength - (Radius + other.Radius) * (Radius + other.Radius);
        }

    }

}
