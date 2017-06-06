using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
    public class Tile {
        public const float size = 20;

        int x, y;

        List<Collider> _colliders;
        public List<Collider> colliders { get { return _colliders; } }

        public Tile (int x, int y) {
            // TODO: Complete member initialization
            this.x = x;
            this.y = y;
            _colliders = new List<Collider>();
        }

        public void AddCollider (Collider collider) {
            _colliders.Add(collider);
        }

        public void RemoveCollider (Collider collider) {
            _colliders.Remove(collider);
        }
    }
}