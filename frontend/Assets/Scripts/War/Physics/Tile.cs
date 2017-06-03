using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    public class Tile {
        public const float size = 20;

        int x, y;

        public Tile (int x, int y) {
            // TODO: Complete member initialization
            this.x = x;
            this.y = y;
        }

        public void AddCollider (Collider collider) {
            throw new NotImplementedException();
        }

        public void RemoveCollider (Collider collider) {
            throw new NotImplementedException();
        }
    }
}