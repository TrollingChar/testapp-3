﻿namespace W3 {

    public abstract class Primitive {

        public virtual float Locate (Primitive other, XY offset) {
            var cp = other as CirclePrimitive;
            return cp == null ? 0 : LocateCircle(cp, offset);
        }


        public virtual float LocateCircle (CirclePrimitive other, XY offset) {
            return 0;
        }

    }

}
