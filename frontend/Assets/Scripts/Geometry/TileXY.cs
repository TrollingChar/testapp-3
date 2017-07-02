namespace Geometry {

    public class TileXY {

        public int X, Y;


        public TileXY (int x, int y) {
            this.X = x;
            this.Y = y;
        }


        public static bool operator == (TileXY a, TileXY b) {
            return a.X == b.X && a.Y == b.Y;
        }


        public static bool operator != (TileXY a, TileXY b) {
            return a.X != b.X || a.Y != b.Y;
        }


        public override bool Equals (object obj) {
            return this == obj as TileXY;
        }


        public override int GetHashCode () {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

    }

}
