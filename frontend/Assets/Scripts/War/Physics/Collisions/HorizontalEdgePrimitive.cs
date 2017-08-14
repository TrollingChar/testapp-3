using Geometry;


namespace War.Physics.Collisions {

    public class HorizontalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        private readonly bool _facingDown;
        private readonly float _y;


        private HorizontalEdgePrimitive (float y, bool down) {
            _y = y;
            _facingDown = down;
        }


        public static HorizontalEdgePrimitive Down (float y) {
            return new HorizontalEdgePrimitive(y, true);
        }


        public static HorizontalEdgePrimitive Up (float y) {
            return new HorizontalEdgePrimitive(y, false);
        }


        public override float LocateCircle (CirclePrimitive other, XY offset) {
            return (_facingDown ? _y - other.Center.Y : other.Center.Y - _y) - other.Radius;
        }

    }

}
