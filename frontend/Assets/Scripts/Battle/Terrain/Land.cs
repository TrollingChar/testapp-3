﻿using Battle.Generation;
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


//        public Collision CastRay (XY beg, XY v, float width = 0) {
//            float _;
//            return CastRay(beg, v, out _, width);
//        }

        /*
        public Collision CastRay (XY beg, XY v, out float dist, float width = 0) {
            XY bp, ep, normal = XY.Zero, end = beg + v;
            Primitive pr = null;

            dist = 1;

            int w = _array.GetLength(0);
            int h = _array.GetLength(1);

            if (v.X != 0) {
                bp = beg;
                ep = bp + v;
                int startX, endX;
                if (v.X < 0) {
                    bp.X -= width;
                    ep.X -= width;
                    // TODO: clamp between 0 and array bound
                    startX = Mathf.FloorToInt(bp.X);
                    endX = Mathf.FloorToInt(ep.X);
                } else {
                    bp.X += width;
                    ep.X += width;
                    startX = Mathf.CeilToInt(bp.X);
                    endX = Mathf.CeilToInt(ep.X);
                }
                for (int x = startX; x != endX;) {
                    float d = Geom.CastRayToVertical(bp, v, x);
                    int y = Mathf.FloorToInt(bp.Y + v.Y * d);
                    if (v.X < 0) --x;
                    if (d < 1 && this[x, y] != 0) {
                        dist = d;
                        v *= d;
                        if (v.X < 0) {
                            normal = XY.Right;
                            pr = VerticalEdgePrimitive.Right(++x);
                        } else {
                            normal = XY.Left;
                            pr = VerticalEdgePrimitive.Left(x);
                        }
                        break;
                    }
                    if (v.X > 0) ++x;
                }
            }
            if (v.Y != 0) {
                bp = beg;
                ep = bp + v;
                int startY, endY;
                if (v.Y < 0) {
                    bp.Y -= width;
                    ep.Y -= width;
                    // TODO: clamp between 0 and array bound
                    startY = Mathf.FloorToInt(bp.Y);
                    endY = Mathf.FloorToInt(ep.Y);
                } else {
                    bp.Y += width;
                    ep.Y += width;
                    startY = Mathf.CeilToInt(bp.Y);
                    endY = Mathf.CeilToInt(ep.Y);
                }
                for (int y = startY; y != endY;) {
                    float d = Geom.CastRayToHorizontal(bp, v, y);
                    int x = Mathf.FloorToInt(bp.X + v.X * d);
                    if (v.Y < 0) --y;
                    if (d < 1 && this[x, y] != 0) {
                        dist *= d;
                        v *= d;
                        if (v.Y < 0) {
                            normal = XY.Up;
                            pr = HorizontalEdgePrimitive.Up(++y);
                        } else {
                            normal = XY.Down;
                            pr = HorizontalEdgePrimitive.Down(y);
                        }
                        break;
                    }
                    if (v.Y > 0) ++y;
                }
            }
            if (width > 0) {
                // и теперь обойти вершины в тайлах.
                float d = 1;

                bp = beg;
                ep = bp + v;

                var aabb = new AABBF(
                    Mathf.Min(bp.X, ep.X) - width,
                    Mathf.Max(bp.X, ep.X) + width,
                    Mathf.Min(bp.Y, ep.Y) - width,
                    Mathf.Max(bp.Y, ep.Y) + width
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (var pt in Tiles[x, y].Vertices) {
                        float dd = Geom.CastRayToCircle(bp, v, pt, width);
                        if (dd >= d) continue;
                        d = dd;
                        normal = bp + v * d - pt;
                        pr = CirclePrimitive.New(pt);
                    }
                }
                v *= d;
                dist *= d;
            }

            return dist < 1
                ? new Collision(v, normal, null, null)
                : null;
        }

/*
        public Collision CastRectRay (float left, float right, float bottom, float top, XY v) {
            List<XY> points;
            float dist = 1;
            Collision min = null;

            if (v.X < 0) {
                points = new List<XY> {new XY(left, top), new XY(left, bottom)};
                if (v.Y < 0) {
                    points.Add(new XY(right, bottom));
                } else if (v.Y > 0) points.Add(new XY(right, top));
            } else if (v.X > 0) {
                points = new List<XY> {new XY(right, top), new XY(right, bottom)};
                if (v.Y < 0) {
                    points.Add(new XY(left, bottom));
                } else if (v.Y > 0) points.Add(new XY(left, top));
            } else {
                if (v.Y < 0) {
                    points = new List<XY> {new XY(left, bottom), new XY(right, bottom)};
                } else if (v.Y > 0) {
                    points = new List<XY> {new XY(left, top), new XY(right, top)};
                } else {
                    return null;
                }
            }

            foreach (var pt in points) {
                float d;
                var temp = CastRay(pt, v * dist, out d);
                if (d >= 1f) continue;
                dist *= d;
                v *= d;
                min = temp;
            }

            // проверили вершины прямоугольника, теперь осталось проверить стороны
            float dd = 1;
            var normal = XY.Zero;
            Primitive pr1 = null, pr2 = null;

            if (v.X != 0) {
                float xx = v.X < 0 ? left : right;
                var a = new XY(xx, bottom);
                var b = new XY(xx, top);
                var n = v.X < 0 ? XY.Right : XY.Left;
                Primitive current = v.X < 0
                    ? VerticalEdgePrimitive.Left(xx)
                    : VerticalEdgePrimitive.Right(xx);

                var aabb = new AABBF(
                    xx + Mathf.Min(0, v.X),
                    xx + Mathf.Max(0, v.X),
                    Mathf.Min(a.Y, b.Y) + Mathf.Min(0, v.Y),
                    Mathf.Max(a.Y, b.Y) + Mathf.Max(0, v.Y)
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (var pt in Tiles[x, y].Vertices) {
                        float d = Geom.CastRayToVertical(pt, -v, xx);
                        float yy = pt.Y - v.Y * d;
                        if (yy <= bottom || yy >= top || d >= dd) continue;
                        dd = d;
                        normal = n;
                        pr1 = current;
                        pr2 = CirclePrimitive.New(pt);
                    }
                }
            }

            if (v.Y != 0) {
                float yy = v.Y < 0 ? bottom : top;
                var a = new XY(left, yy);
                var b = new XY(right, yy);
                var n = v.Y < 0 ? XY.Up : XY.Down;
                Primitive current = v.Y < 0
                    ? HorizontalEdgePrimitive.Down(yy)
                    : HorizontalEdgePrimitive.Up(yy);

                var aabb = new AABBF(
                    Mathf.Min(a.X, b.X) + Mathf.Min(0, v.X),
                    Mathf.Max(a.X, b.X) + Mathf.Max(0, v.X),
                    yy + Mathf.Min(0, v.Y),
                    yy + Mathf.Max(0, v.Y)
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (var pt in Tiles[x, y].Vertices) {
                        float d = Geom.CastRayToHorizontal(pt, -v, yy);
                        float xx = pt.X - v.X * d;
                        if (xx <= left || xx >= right || d >= dd) continue;
                        dd = d;
                        normal = n;
                        pr1 = current;
                        pr2 = CirclePrimitive.New(pt);
                    }
                }
            }
            dist *= dd;
            v *= dd;

            return dd < 1 ? new Collision(v, normal, null, null) : min;
        }
*/

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