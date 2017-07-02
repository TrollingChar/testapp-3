using Geometry;
using UnityEngine;


namespace War.Physics.Collisions {

    public struct AABBF {

        public float Left, Right, Bottom, Top;


        public AABBF (float left, float right, float bottom, float top) {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }


        public AABBF Expanded (XY v) {
            return new AABBF(
                Left + Mathf.Min(0, v.X),
                Right + Mathf.Max(0, v.X),
                Bottom + Mathf.Min(0, v.Y),
                Top + Mathf.Max(0, v.Y)
            );
        }


        public AABB ToTiles (float tileSize) {
            return new AABB(
                Mathf.FloorToInt(Left / tileSize),
                Mathf.FloorToInt(Right / tileSize) + 1,
                Mathf.FloorToInt(Bottom / tileSize),
                Mathf.FloorToInt(Top / tileSize) + 1
            );
        }

    }


    public struct AABB {

        public int Left, Right, Bottom, Top;


        public AABB (int left, int right, int bottom, int top) {
            Left = left;
            Right = right;
            Bottom = bottom;
            Top = top;
        }

    }

}
