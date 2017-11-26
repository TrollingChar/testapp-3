using System;
using Geometry;


namespace Battle.Physics.Collisions {

    [Obsolete]
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
