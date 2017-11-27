using System;
using Battle.Physics.Collisions;
using Geometry;
using Primitive = Geometry.Primitive;


namespace Collisions {

    public class NewCollision {

        public XY Offset;
        public XY Normal;
        public Collider C1, C2;
        public Primitive P1, P2;


        public NewCollision (XY offset, Primitive p1, Primitive p2) {
            Offset = offset;
            P1 = p1;
            P2 = p2;
        }


        public void ImprovePrecision () {
            throw new NotImplementedException();
        }

    }

}
