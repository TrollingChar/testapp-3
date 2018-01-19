using System.Collections.Generic;
using Geometry;


namespace Battle.Terrain {

    public class LandTiles {

        private readonly Dictionary<TileXY, LandTile> _tiles;


        public LandTiles () {
            _tiles = new Dictionary<TileXY, LandTile>(65536);
        }


        public LandTile this [int x, int y] {
            get {
                LandTile tile;
                return _tiles.TryGetValue(new TileXY(x, y), out tile)
                    ? tile
                    : (_tiles[new TileXY(x, y)] = new LandTile(x, y));
            }
            set { _tiles[new TileXY(x, y)] = value; }
        }

    }

}
