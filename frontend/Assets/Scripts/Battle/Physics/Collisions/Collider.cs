using System.Collections.Generic;
using Battle.Objects;
using Geometry;
using Utils.Singleton;


namespace Battle.Physics.Collisions {

    public abstract class Collider : Component {

        private readonly List<Tile> _tiles;
        private readonly World _world = The<World>.Get();

        public float TangentialBounce, NormalBounce;


        protected Collider () {
            _tiles = new List<Tile>();
            TangentialBounce = 0.9f;
            NormalBounce = 0.5f;
        }


        public abstract AABBF AABB { get; }


        public void FreeTiles () {
            foreach (var tile in _tiles) tile.RemoveCollider(this);
            _tiles.Clear();
        }


        public void OccupyTiles () {
            var box = AABB.ToTiles(Tile.Size);
            for (int x = box.Left; x < box.Right; x++)
            for (int y = box.Bottom; y < box.Top; y++) {
                var tile = _world.Tiles[x, y];
                tile.AddCollider(this);
                _tiles.Add(tile);
            }
        }


        public void UpdatePosition () {
            FreeTiles();
            OccupyTiles();
        }


        public HashSet<Collider> FindObstacles (World world, XY v) {
            var box = AABB.Expanded(v).ToTiles(Tile.Size);
            var result = new HashSet<Collider>();

            for (int x = box.Left; x < box.Right; x++)
            for (int y = box.Bottom; y < box.Top; y++) {
                foreach (var c in _world.Tiles[x, y].Colliders) {
                    result.Add(c);
                }
            }

            return result;
        }


        public HashSet<Collider> FindOverlapping (World world) {
            var box = AABB.ToTiles(Tile.Size);
            var result = new HashSet<Collider>();

            for (int x = box.Left; x < box.Right; x++)
            for (int y = box.Bottom; y < box.Top; y++) {
                foreach (var c in _world.Tiles[x, y].Colliders) {
                    if (OverlapsWith(c)) result.Add(c);
                }
            }
            return result;
        }


        public abstract Collision CollideWith (Collider c, XY velocity);
        public abstract Collision CollideWithCircle (CircleCollider c, XY velocity);
        public abstract Collision CollideWithBox (BoxCollider c, XY velocity);
        public abstract Collision CollideWithLand (Land land, XY v);

        public abstract bool OverlapsWith (Collider c);
        public abstract bool OverlapsWithCircle (CircleCollider c);
        public abstract bool OverlapsWithBox (BoxCollider c);
        public abstract bool OverlapsWithLand (Land land);

    }

}
