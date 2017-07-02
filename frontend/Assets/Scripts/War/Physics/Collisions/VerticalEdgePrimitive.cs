namespace W3 {

    public class VerticalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        private bool facingLeft;
        private float x;


        private VerticalEdgePrimitive (float x, bool left) {
            this.x = x;
            facingLeft = left;
        }


        public static VerticalEdgePrimitive Left (float x) {
            return new VerticalEdgePrimitive(x, true);
        }


        public static VerticalEdgePrimitive Right (float x) {
            return new VerticalEdgePrimitive(x, false);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (facingLeft ? x - other.center.x : other.center.x - x) - other.radius;
        }

    }

}
