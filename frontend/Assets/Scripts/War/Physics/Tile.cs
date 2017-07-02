using System.Collections.Generic;
using Collider = War.Physics.Collisions.Collider;


namespace War.Physics {

    public class Tile {

        public const float Size = 20;

        private int _x, _y;

        public List<Collider> Colliders { get; private set; }


        public Tile (int x, int y) {
            this._x = x;
            this._y = y;
            Colliders = new List<Collider>();
        }


        public void AddCollider (Collider collider) {
            Colliders.Add(collider);
        }


        public void RemoveCollider (Collider collider) {
            Colliders.Remove(collider);
        }

    }

}
