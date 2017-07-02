using System.Collections.Generic;
using Geometry;


namespace War.Physics {

    public class LandTile {

        public const int Size = 20;

        private int _x, _y;
        public int Land;
        public List<XY> Vertices;


        public LandTile (int x, int y) {
            this._x = x;
            this._y = y;
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
            this.Land = 0;
            for (int ix = _x * Size, xx = ix + Size; ix < xx; ix++)
            for (int iy = _y * Size, yy = iy + Size; iy < yy; iy++) {
                this.Land += land[_x, _y];
            }
        }


        private void CalculateVertices (Land land) {
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
