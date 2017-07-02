using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public class Tile {
        public const float size = 20;

        private int x, y;

        public List<Collider> colliders { get; private set; }

        public Tile (int x, int y) {
            this.x = x;
            this.y = y;
            colliders = new List<Collider>();
        }

        public void AddCollider (Collider collider) {
            colliders.Add(collider);
        }

        public void RemoveCollider (Collider collider) {
            colliders.Remove(collider);
        }
    }
}
