using System.Collections.Generic;
using Geometry;


namespace War.Physics {

    internal class LandTiles {

        private Dictionary<TileXY, LandTile> tiles;


        public LandTiles () {
            tiles = new Dictionary<TileXY, LandTile>(65536);
        }


        public LandTile this [int x, int y] {
            get {
                LandTile tile;
                return tiles.TryGetValue(new TileXY(x, y), out tile)
                    ? tile
                    : (tiles[new TileXY(x, y)] = new LandTile(x, y));
            }
            set { tiles[new TileXY(x, y)] = value; }
        }

    }

}
