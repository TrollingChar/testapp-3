using System.Collections.Generic;

namespace W3 {
    public class LandTile {
        public const int size = 20;

        private int x, y;
        public int land;
        public List<XY> vertices;

        public LandTile (int x, int y) {
            this.x = x;
            this.y = y;
            vertices = new List<XY>();
        }

        public void Erase() {
            land = 0;
            vertices.Clear();
        }

        public void Fill () {
            land = size * size;
            vertices.Clear();
            vertices.Add(new XY(x * size, y * size));
            vertices.Add(new XY(x * size + size, y * size));
            vertices.Add(new XY(x * size + size, y * size + size));
            vertices.Add(new XY(x * size, y * size + size));
        }

        public void Recalculate (Land land) {
            CalculateLand(land);
            CalculateVertices(land);
        }

        void CalculateLand (Land land) {
            this.land = 0;
            for (int ix = x * size, xx = ix + size; ix < xx; ix++) {
                for (int iy = y * size, yy = iy + size; iy < yy; iy++) {
                    this.land += land[x, y];
                }
            }
        }

        void CalculateVertices (Land land) {
            for (int ix = x * size, xx = ix + size; ix <= xx; ix++) {
                for (int iy = y * size, yy = iy + size; iy <= yy; iy++) {
                    int temp = land[ix - 1, iy]
                             + land[ix - 1, iy - 1]
                             + land[ix, iy - 1]
                             + land[ix, iy];
                    if (temp == 1) vertices.Add(new XY(ix, iy));
                }
            }
        }
    }
}
