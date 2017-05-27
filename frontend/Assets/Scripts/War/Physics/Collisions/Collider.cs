using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    public class Collider {
        public Object obj;
        public float tangentialBounce, normalBounce;

        public void UpdatePosition () {
            throw new NotImplementedException();
        }

        public HashSet<Collider> FindObstacles (World world, XY v) {
            throw new NotImplementedException();
        }

        public Collision CollideWith (Collider c, XY velocity) {
            throw new NotImplementedException();
        }

        public Collision CollideWithLand (Land land, XY v) {
            throw new NotImplementedException();
        }

        internal void FreeTiles () {
            throw new NotImplementedException();
        }
    }
}
