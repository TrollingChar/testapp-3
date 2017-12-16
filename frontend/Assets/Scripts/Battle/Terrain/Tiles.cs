using System.Collections.Generic;
using Geometry;


namespace Battle.Physics {

    public class Tiles {

        private readonly Dictionary<TileXY, Tile> _tiles;


        public Tiles () {
            _tiles = new Dictionary<TileXY, Tile>();
        }


        public Tile this [int x, int y] {
            get {
                Tile tile;
                return _tiles.TryGetValue(new TileXY(x, y), out tile)
                    ? tile
                    : (_tiles[new TileXY(x, y)] = new Tile(x, y));
            }
            set { _tiles[new TileXY(x, y)] = value; }
        }

    }

}
