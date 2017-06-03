using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace W3 {
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
            if (!(obj is TileXY)) return false;
            TileXY v = (TileXY)obj;
            return this == v;
        }

        public override int GetHashCode () {
            return x.GetHashCode() ^ y.GetHashCode() << 2;
        }
    }
}