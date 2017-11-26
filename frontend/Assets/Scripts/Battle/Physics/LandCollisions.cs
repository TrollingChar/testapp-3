using Battle.Physics.Collisions;
using Geometry;


namespace Battle.Physics {

    public partial class Land {

        public void ApproxCollision (Circle c, XY v) {
            var v0 = v;
            if (v.X > 0) A();
            if (v.X < 0) ;
            if (v.Y > 0) ;
            if (v.Y < 0) ;
        }


        private void A () {
            
        }

    }

}