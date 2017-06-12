using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    public class HorizontalEdgePrimitive : Primitive {

        // todo: use object pool for primitives

        bool facingDown;
        float y;

        HorizontalEdgePrimitive (float y, bool down) {
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
