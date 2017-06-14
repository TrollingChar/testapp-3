using System;
using System.Collections.Generic;
using UnityEngine;

namespace W3 {
    public abstract class Collider {
        public Object obj;
        public float tangentialBounce, normalBounce;
        List<Tile> tiles;

        public abstract AABBF aabb { get; }

        public Collider () {
            tiles = new List<Tile>();
            tangentialBounce = 0.9f;
            normalBounce = 0.5f;
        }

        public void FreeTiles () {
            foreach (var tile in tiles) tile.RemoveCollider(this);
            tiles.Clear();
        }

        public void OccupyTiles () {
            var box = aabb.ToTiles(Tile.size);
            for (int x = box.left; x < box.right; x++) {
                for (int y = box.bottom; y < box.top; y++) {
                    Tile tile = Core.bf.world.tiles[x, y];
                    tile.AddCollider(this);
                    tiles.Add(tile);
                }
            }
        }

        public void UpdatePosition () {
            FreeTiles();
            OccupyTiles();
        }

        public HashSet<Collider> FindObstacles (World world, XY v) {
            var box = aabb.Expanded(v).ToTiles(Tile.size);
            var result = new HashSet<Collider>();
            return result;
        }

        public HashSet<Collider> FindObstacles (World world) {
            var box = aabb.ToTiles(Tile.size);
            var result = new HashSet<Collider>();
            return result;
        }

        public abstract Collision CollideWith (Collider c, XY velocity);
        public abstract Collision CollideWithCircle (CircleCollider c, XY velocity);
        //public abstract Collision CollideWithBox (BoxCollider c, XY velocity);
        //public abstract Collision CollideWithPolygon (PolygonCollider c, XY velocity);
        public abstract Collision CollideWithLand (Land land, XY v);
    }
}
