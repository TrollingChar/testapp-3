﻿using System.Collections.Generic;
using Geometry;


namespace Battle.Terrain {

    public class LandTile {

        public const int Size = 20;

        private readonly int _x;
        private readonly int _y;
        public int Land;
        public List<XY> Vertices;


        public LandTile (int x, int y) {
            _x = x;
            _y = y;
            Vertices = new List<XY>();
        }


        public void Erase () {
            Land = 0;
            Vertices.Clear();
        }


        public void Fill () {
            Land = Size * Size;
            Vertices.Clear();
            Vertices.Add(new XY(_x * Size, _y * Size));
            Vertices.Add(new XY(_x * Size + Size, _y * Size));
            Vertices.Add(new XY(_x * Size + Size, _y * Size + Size));
            Vertices.Add(new XY(_x * Size, _y * Size + Size));
        }


        public void Recalculate (Land land) {
            CalculateLand(land);
            CalculateVertices(land);
        }


        private void CalculateLand (Land land) {
            Land = 0;
            for (int ix = _x * Size, xx = ix + Size; ix < xx; ix++)
            for (int iy = _y * Size, yy = iy + Size; iy < yy; iy++) {
                Land += land[ix, iy];
            }
        }


        private void CalculateVertices (Land land) {
            Vertices.Clear();
            for (int ix = _x * Size, xx = ix + Size; ix <= xx; ix++)
            for (int iy = _y * Size, yy = iy + Size; iy <= yy; iy++) {
                int temp
                    = land[ix - 1, iy]
                    + land[ix - 1, iy - 1]
                    + land[ix, iy - 1]
                    + land[ix, iy];
                if (temp == 1) Vertices.Add(new XY(ix, iy));
            }
        }

    }

}
