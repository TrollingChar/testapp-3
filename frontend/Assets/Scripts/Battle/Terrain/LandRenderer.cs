using System.Collections.Generic;
using System.Linq;
using Geometry;
using UnityEngine;


namespace Battle.Terrain {

    public class LandRenderer : MonoBehaviour {

//        private readonly Transform _parent;
        private Dictionary<TileXY, LandRendererTile> _tiles = new Dictionary<TileXY, LandRendererTile>();


//        public LandRenderer (Transform parent) {
//            _parent = parent;
//            _tiles;
//        }


        private LandRendererTile this [int x, int y] {
            get {
                LandRendererTile tile;
                return _tiles.TryGetValue(new TileXY(x, y), out tile)
                    ? tile
                    : (_tiles[new TileXY(x, y)] = new LandRendererTile(transform, x, y));
            }
            set { _tiles[new TileXY(x, y)] = value; }
        }


        public void SetPixel (int x, int y, Color color) {
            this[x / LandRendererTile.Size, y / LandRendererTile.Size].SetPixel
                (x % LandRendererTile.Size, y % LandRendererTile.Size, color);
        }


        public void Apply () {
            _tiles = _tiles.Where(kvp => !kvp.Value.IsEmpty).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            foreach (var tile in _tiles) tile.Value.Apply();
        }


        private void OnDestroy () {
            foreach (var tile in _tiles) tile.Value.Clear();
            _tiles.Clear();
        }

    }

}