using Geometry;


namespace Battle.Physics.Collisions {

    public class VerticalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        private readonly bool _facingLeft;
        private readonly float _x;


        private VerticalEdgePrimitive (float x, bool left) {
            _x = x;
            _facingLeft = left;
        }


        public static VerticalEdgePrimitive Left (float x) {
            return new VerticalEdgePrimitive(x, true);
        }


        public static VerticalEdgePrimitive Right (float x) {
            return new VerticalEdgePrimitive(x, false);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (_facingLeft ? _x - other.Center.X : other.Center.X - _x) - other.Radius;
        }

    }

}
