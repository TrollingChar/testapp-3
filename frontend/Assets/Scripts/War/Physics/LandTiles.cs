﻿using System.Collections.Generic;
using Geometry;


namespace War.Physics {

    public class LandTiles {

        private Dictionary<TileXY, LandTile> _tiles;


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
