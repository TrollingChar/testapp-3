using System.Collections.Generic;
using Collider = War.Physics.Collisions.Collider;


namespace War.Physics {
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
