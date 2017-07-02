using Geometry;


namespace War.Physics.Collisions {

    public class HorizontalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        private bool _facingDown;
        private float _y;


        private HorizontalEdgePrimitive (float y, bool down) {
            this._y = y;
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
