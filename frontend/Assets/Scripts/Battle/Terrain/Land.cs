using Battle.Generation;
using Collisions;
using Geometry;
using UnityEngine;


namespace Battle.Physics {

    public partial class Land {

        private byte[,] _array;

        //private Texture2D _tex;
        private LandRenderer _landRenderer;

        private int _progress, _fullProgress;

        public float TangentialBounce, NormalBounce;


        public Land (LandGen gen, LandRenderer renderer, Texture2D landTexture) {
            InitArray(gen);
            InitTiles();
            InitTexture(renderer, landTexture);
            TangentialBounce = 0.9f;
            NormalBounce = 0.5f;
        }


        public LandTiles Tiles { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int WidthInTiles { get; private set; }
        public int HeightInTiles { get; private set; }


        public byte this [int x, int y] {
            get {
                return x < 0 || y < 0 || x >= Width || y >= Height
                    ? (byte) 0
                    : _array[x, y];
            }
            // set { array[x, y] = value == 0 ? 0 : 1; }
        }


        private void InitArray (LandGen gen) {
            _array = gen.Array;

            Width = _array.GetLength(0);
            Height = _array.GetLength(1);
        }


        private void InitTexture (Texture2D landTexture, SpriteRenderer renderer) {}


        private void InitTexture (LandRenderer landRenderer, Texture2D landTexture) {
            _landRenderer = landRenderer;
//            _tex = new Texture2D(Width, Height);
            for (int x = 0; x < Width; ++x)
            for (int y = 0; y < Height; ++y) {
                _landRenderer.SetPixel(x, y, _array[x, y] == 0 ? Color.clear : landTexture.GetPixel(x & 0xff, y & 0xff));
            }
            _landRenderer.Apply();
            //renderer.sprite = Sprite.Create(_tex, new Rect(0, 0, Width, Height), new Vector2(0, 0), 1f);
        }


        private void InitTiles () {
            WidthInTiles = Width / LandTile.Size;
            if (Width % LandTile.Size != 0) ++WidthInTiles;

            HeightInTiles = Height / LandTile.Size;
            if (Height % LandTile.Size != 0) ++HeightInTiles;

            Tiles = new LandTiles();
            for (int x = 0; x < WidthInTiles; x++)
            for (int y = 0; y < HeightInTiles; y++) {
                var tile = new LandTile(x, y);
                tile.Recalculate(this);
                Tiles[x, y] = tile;
            }
        }


        public void DestroyTerrain (XY center, float radius) {
            float sqrRadius = radius * radius;

            // offset the center because of the alignment of pixels
            center -= new XY(0.5f, 0.5f);

            // affect array and texture
            int left = Mathf.Max(0, Mathf.FloorToInt(center.X - radius));
            int right = Mathf.Min(Width, 1 + Mathf.FloorToInt(center.X + radius));
            int bottom = Mathf.Max(0, Mathf.FloorToInt(center.Y - radius));
            int top = Mathf.Min(Height, 1 + Mathf.FloorToInt(center.Y + radius));

//            var pixels = _tex.GetPixels(left, bottom, right - left, top - bottom);

            for (int x = left; x < right; x++)
            for (int y = bottom; y < top; y++) {
                if (XY.SqrDistance(center, new XY(x, y)) > sqrRadius) continue;
                _array[x, y] = 0;
//                int arrayIndex = (x - left) + (y - bottom) * (right - left);
//                pixels[arrayIndex] = Random.ColorHSV();
                _landRenderer.SetPixel(x, y, Color.clear);
            }
//            _tex.SetPixels(left, bottom, right - left, top - bottom, pixels);
            _landRenderer.Apply();

            var aabb = new AABBF(left, right, bottom, top).ToTiles(LandTile.Size);

            // affect tiles
            bool holeLargeEnough = sqrRadius * 2 > LandTile.Size * LandTile.Size;
            for (int x = aabb.Left; x < aabb.Right; x++)
            for (int y = aabb.Bottom; y < aabb.Top; y++) {
                var tile = Tiles[x, y];

                if (tile.Land == 0 && tile.Vertices.Count == 0) continue;

                // check tile corners
                byte temp = 0;
                if (XY.SqrDistance(center, new XY(x * LandTile.Size - 1, y * LandTile.Size - 1)) > sqrRadius) temp++;
                if (XY.SqrDistance(center, new XY((x + 1) * LandTile.Size, y * LandTile.Size - 1)) > sqrRadius) temp++;
                if (XY.SqrDistance(center, new XY(x * LandTile.Size - 1, (y + 1) * LandTile.Size)) > sqrRadius) temp++;
                if (XY.SqrDistance(center, new XY((x + 1) * LandTile.Size, (y + 1) * LandTile.Size)) > sqrRadius) temp++;

                // if not affected do nothing (hole must be large enough for this to work)
                if (temp == 4 && holeLargeEnough) continue;

                // if entirely inside circle
                if (temp == 0) tile.Erase();

                // if only partially affected
                else tile.Recalculate(this);
            }

        }

    }

}
