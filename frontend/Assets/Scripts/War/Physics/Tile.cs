using System.Collections.Generic;
using War.Physics.Collisions;


namespace War.Physics {

    public class Tile {

        public const float Size = 20;

        private int _x, _y;


        public Tile (int x, int y) {
            _x = x;
            _y = y;
            Colliders = new List<Collider>();
        }


        public List<Collider> Colliders { get; private set; }


        public void AddCollider (Collider collider) {
            Colliders.Add(collider);
        }


        public void RemoveCollider (Collider collider) {
            Colliders.Remove(collider);
        }

    }

}
