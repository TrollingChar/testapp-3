using System.Collections;
using System.Collections.Generic;

namespace W3 {
    public class Tiles {
        Dictionary<TileXY, Tile> tiles;
        public Tile this[int x, int y] {
            get {
                var tile = tiles[new TileXY(x, y)];
                return tile ?? new Tile(x, y);
            }
            set {
                tiles[new TileXY(x, y)] = value;
            }
        }
    }
}
