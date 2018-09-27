using Battle.Objects;


namespace Geometry {

    public struct Box {

        public float Top;
        public float Left;
        public float Right;
        public float Bottom;


        public Box (float left, float right, float bottom, float top) {
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;
        }


        public bool Contains (XY p) {
            // неравенство нестрогое так как в камере нужно именно такое, todo: универсальность
            return p.X >= Left && p.X <= Right && p.Y >= Bottom && p.Y <= Top;
        }

    }

}
