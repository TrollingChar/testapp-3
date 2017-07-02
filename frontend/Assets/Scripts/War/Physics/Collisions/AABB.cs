using UnityEngine;


namespace W3 {

    public struct AABBF {

        public float left, right, bottom, top;


        public AABBF (float left, float right, float bottom, float top) {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }


        public AABBF Expanded (XY v) {
            return new AABBF(
                left + Mathf.Min(0, v.x),
                right + Mathf.Max(0, v.x),
                bottom + Mathf.Min(0, v.y),
                top + Mathf.Max(0, v.y)
            );
        }


        public AABB ToTiles (float tileSize) {
            return new AABB(
                Mathf.FloorToInt(left / tileSize),
                Mathf.FloorToInt(right / tileSize) + 1,
                Mathf.FloorToInt(bottom / tileSize),
                Mathf.FloorToInt(top / tileSize) + 1
            );
        }

    }


    public struct AABB {

        public int left, right, bottom, top;


        public AABB (int left, int right, int bottom, int top) {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

    }

}
