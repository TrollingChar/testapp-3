namespace Geometry {

    public class TileXY {

        public int x, y;


        public TileXY (int x, int y) {
            this.x = x;
            this.y = y;
        }


        public static bool operator == (TileXY a, TileXY b) {
            return a.x == b.x && a.y == b.y;
        }


        public static bool operator != (TileXY a, TileXY b) {
            return a.x != b.x || a.y != b.y;
        }


        public override bool Equals (object obj) {
            return this == obj as TileXY;
        }


        public override int GetHashCode () {
            return x.GetHashCode() ^ y.GetHashCode() << 2;
        }

    }

}
