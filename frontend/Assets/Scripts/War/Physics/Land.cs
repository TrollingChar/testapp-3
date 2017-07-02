using System.Collections.Generic;
using Geometry;
using UnityEngine;
using War.Physics.Collisions;
using Collision = War.Physics.Collisions.Collision;


namespace War.Physics {

    public class Land {

        private int _progress, _fullProgress;

        private byte[,] _array;
        private LandTiles _tiles;
        private int _width, _height; // do not make them odd
        private int _widthInTiles, _heightInTiles;

        public float TangentialBounce, NormalBounce;


        public Land (LandGen gen, Texture2D landTexture, SpriteRenderer renderer) {
            InitArray(gen);
            InitTiles();
            InitTexture(landTexture, renderer);
            TangentialBounce = 0.9f;
            NormalBounce = 0.5f;
        }


        private void InitArray (LandGen gen) {
            _array = gen.Array;

            _width = _array.GetLength(0);
            _height = _array.GetLength(1);
        }


        private void InitTexture (Texture2D landTexture, SpriteRenderer renderer) {
            var tex = new Texture2D(_width, _height);
            for (int x = 0; x < _width; ++x)
            for (int y = 0; y < _height; ++y) {
                tex.SetPixel(x, y, _array[x, y] == 0 ? Color.clear : landTexture.GetPixel(x & 0xff, y & 0xff));
            }
            tex.Apply();
            renderer.sprite = Sprite.Create(tex, new Rect(0, 0, _width, _height), new Vector2(0, 0), 1f);
        }


        private void InitTiles () {
            _widthInTiles = _width / LandTile.Size;
            if (_width % LandTile.Size != 0) ++_widthInTiles;
            _heightInTiles = _height / LandTile.Size;
            if (_height % LandTile.Size != 0) ++_heightInTiles;

            _tiles = new LandTiles();
            for (int x = 0; x < _widthInTiles; x++)
            for (int y = 0; y < _heightInTiles; y++) {
                var tile = new LandTile(x, y);
                tile.Recalculate(this);
                _tiles[x, y] = tile;
            }
        }


        public byte this [int x, int y] {
            get {
                return x < 0 || y < 0 || x >= _width || y >= _height
                    ? (byte) 0
                    : _array[x, y];
            }
            // set { array[x, y] = value == 0 ? 0 : 1; }
        }


        public Collision CastRay (XY beg, XY v, float width = 0) {
            float _;
            return CastRay(beg, v, out _, width);
        }


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
                    // todo: clamp between 0 and array bound
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
                    // todo: clamp between 0 and array bound
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

                AABB aabb = new AABBF(
                    Mathf.Min(bp.X, ep.X) - width,
                    Mathf.Max(bp.X, ep.X) + width,
                    Mathf.Min(bp.Y, ep.Y) - width,
                    Mathf.Max(bp.Y, ep.Y) + width
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (XY pt in _tiles[x, y].Vertices) {
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
                ? new Collision(v, normal, null, null, CirclePrimitive.New(beg, width), pr)
                : null;
        }


        public Collision CastRectRay (float left, float right, float bottom, float top, XY v) {
            List<XY> points;
            float dist = 1;
            Collision min = null;

            if (v.X < 0) {
                points = new List<XY> {new XY(left, top), new XY(left, bottom)};
                if (v.Y < 0) points.Add(new XY(right, bottom));
                else if (v.Y > 0) points.Add(new XY(right, top));
            } else if (v.X > 0) {
                points = new List<XY> {new XY(right, top), new XY(right, bottom)};
                if (v.Y < 0) points.Add(new XY(left, bottom));
                else if (v.Y > 0) points.Add(new XY(left, top));
            } else {
                if (v.Y < 0) points = new List<XY> {new XY(left, bottom), new XY(right, bottom)};
                else if (v.Y > 0) points = new List<XY> {new XY(left, top), new XY(right, top)};
                else return null;
            }

            foreach (XY pt in points) {
                float d;
                var temp = CastRay(pt, v * dist, out d);
                if (d >= 1f) continue;
                dist *= d;
                v *= d;
                min = temp;
            }

            // проверили вершины прямоугольника, теперь осталось проверить стороны
            float dd = 1;
            XY normal = XY.Zero;
            Primitive pr1 = null, pr2 = null;

            if (v.X != 0) {
                float xx = v.X < 0 ? left : right;
                XY a = new XY(xx, bottom);
                XY b = new XY(xx, top);
                XY n = v.X < 0 ? XY.Right : XY.Left;
                Primitive current = v.X < 0
                    ? VerticalEdgePrimitive.Left(xx)
                    : VerticalEdgePrimitive.Right(xx);

                AABB aabb = new AABBF(
                    xx + Mathf.Min(0, v.X),
                    xx + Mathf.Max(0, v.X),
                    Mathf.Min(a.Y, b.Y) + Mathf.Min(0, v.Y),
                    Mathf.Max(a.Y, b.Y) + Mathf.Max(0, v.Y)
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (XY pt in _tiles[x, y].Vertices) {
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
                XY a = new XY(left, yy);
                XY b = new XY(right, yy);
                XY n = v.Y < 0 ? XY.Up : XY.Down;
                Primitive current = v.Y < 0
                    ? HorizontalEdgePrimitive.Down(yy)
                    : HorizontalEdgePrimitive.Up(yy);

                AABB aabb = new AABBF(
                    Mathf.Min(a.X, b.X) + Mathf.Min(0, v.X),
                    Mathf.Max(a.X, b.X) + Mathf.Max(0, v.X),
                    yy + Mathf.Min(0, v.Y),
                    yy + Mathf.Max(0, v.Y)
                ).ToTiles(LandTile.Size);

                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (XY pt in _tiles[x, y].Vertices) {
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

            return dd < 1 ? new Collision(v, normal, null, null, pr1, pr2) : min;
        }

    }

}
