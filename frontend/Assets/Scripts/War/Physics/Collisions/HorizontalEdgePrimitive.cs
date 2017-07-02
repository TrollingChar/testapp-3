namespace W3 {

    public class HorizontalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        private bool facingDown;
        private float y;


        private HorizontalEdgePrimitive (float y, bool down) {
            this.y = y;
            facingDown = down;
        }


        public static HorizontalEdgePrimitive Down (float y) {
            return new HorizontalEdgePrimitive(y, true);
        }


        public static HorizontalEdgePrimitive Up (float y) {
            return new HorizontalEdgePrimitive(y, false);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (facingDown ? y - other.center.y : other.center.y - y) - other.radius;
        }

    }

}
