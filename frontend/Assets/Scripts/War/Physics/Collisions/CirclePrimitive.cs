using Geometry;


namespace War.Physics.Collisions {

    public class CirclePrimitive : Primitive {

        public XY center;
        public float radius;


        private CirclePrimitive (XY o, float r) {
            center = o;
            radius = r;
        }


        public static CirclePrimitive New (XY center, float radius = 0) {
            return new CirclePrimitive(center, radius);
        }


        public override float Locate (Primitive other, XY offset) {
            return other.LocateCircle(this, -offset);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (center - other.center).sqrLength - (radius + other.radius) * (radius + other.radius);
        }

    }

}
