using System.Collections.Generic;
using Geometry;


namespace War.Physics {
    public class Tiles {

        private Dictionary<TileXY, Tile> tiles;

        public Tiles () {
            tiles = new Dictionary<TileXY, Tile>();
        }

        public Tile this[int x, int y] {
            get {
                Tile tile;
                return tiles.TryGetValue(new TileXY(x, y), out tile)
                    ? tile
                    : (tiles[new TileXY(x, y)] = new Tile(x, y));
            }
            set { tiles[new TileXY(x, y)] = value; }
        }
    }
}
