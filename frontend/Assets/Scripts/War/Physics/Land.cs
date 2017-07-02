using System.Collections.Generic;
using Geometry;
using UnityEngine;
using War.Physics.Collisions;
using Collision = War.Physics.Collisions.Collision;


namespace War.Physics {

    public class Land {

        private int progress, fullProgress;

        private byte[,] array;
        private LandTiles tiles;
        private int width, height; // do not make them odd
        private int widthInTiles, heightInTiles;

        public float tangentialBounce, normalBounce;


        public Land (LandGen gen, Texture2D landTexture, SpriteRenderer renderer) {
            InitArray(gen);
            InitTiles();
            InitTexture(landTexture, renderer);
            tangentialBounce = 0.9f;
            normalBounce = 0.5f;
        }


        private void InitArray (LandGen gen) {
            array = gen.array;

            width = array.GetLength(0);
            height = array.GetLength(1);
        }


        private void InitTexture (Texture2D landTexture, SpriteRenderer renderer) {
            var tex = new Texture2D(width, height);
            for (int x = 0; x < width; ++x)
            for (int y = 0; y < height; ++y) {
                tex.SetPixel(x, y, array[x, y] == 0 ? Color.clear : landTexture.GetPixel(x & 0xff, y & 0xff));
            }
            tex.Apply();
            renderer.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0, 0), 1f);
        }


        private void InitTiles () {
            widthInTiles = width / LandTile.size;
            if (width % LandTile.size != 0) ++widthInTiles;
            heightInTiles = height / LandTile.size;
            if (height % LandTile.size != 0) ++heightInTiles;

            tiles = new LandTiles();
            for (int x = 0; x < widthInTiles; x++)
            for (int y = 0; y < heightInTiles; y++) {
                var tile = new LandTile(x, y);
                tile.Recalculate(this);
                tiles[x, y] = tile;
            }
        }


        public byte this [int x, int y] {
            get {
                return x < 0 || y < 0 || x >= width || y >= height
                    ? (byte) 0
                    : array[x, y];
            }
            // set { array[x, y] = value == 0 ? 0 : 1; }
        }


        public Collision CastRay (XY beg, XY v, float width = 0) {
            float _;
            return CastRay(beg, v, out _, width);
        }


        public Collision CastRay (XY beg, XY v, out float dist, float width = 0) {
            XY bp, ep, normal = XY.zero, end = beg + v;
            Primitive pr = null;

            dist = 1;

            int w = array.GetLength(0);
            int h = array.GetLength(1);

            if (v.x != 0) {
                bp = beg;
                ep = bp + v;
                int startX, endX;
                if (v.x < 0) {
                    bp.x -= width;
                    ep.x -= width;
                    // todo: clamp between 0 and array bound
                    startX = Mathf.FloorToInt(bp.x);
                    endX = Mathf.FloorToInt(ep.x);
                } else {
                    bp.x += width;
                    ep.x += width;
                    startX = Mathf.CeilToInt(bp.x);
                    endX = Mathf.CeilToInt(ep.x);
                }
                for (int x = startX; x != endX;) {
                    float d = Geom.CastRayToVertical(bp, v, x);
                    int y = Mathf.FloorToInt(bp.y + v.y * d);
                    if (v.x < 0) --x;
                    if (d < 1 && this[x, y] != 0) {
                        dist = d;
                        v *= d;
                        if (v.x < 0) {
                            normal = XY.right;
                            pr = VerticalEdgePrimitive.Right(++x);
                        } else {
                            normal = XY.left;
                            pr = VerticalEdgePrimitive.Left(x);
                        }
                        break;
                    }
                    if (v.x > 0) ++x;
                }
            }
            if (v.y != 0) {
                bp = beg;
                ep = bp + v;
                int startY, endY;
                if (v.y < 0) {
                    bp.y -= width;
                    ep.y -= width;
                    // todo: clamp between 0 and array bound
                    startY = Mathf.FloorToInt(bp.y);
                    endY = Mathf.FloorToInt(ep.y);
                } else {
                    bp.y += width;
                    ep.y += width;
                    startY = Mathf.CeilToInt(bp.y);
                    endY = Mathf.CeilToInt(ep.y);
                }
                for (int y = startY; y != endY;) {
                    float d = Geom.CastRayToHorizontal(bp, v, y);
                    int x = Mathf.FloorToInt(bp.x + v.x * d);
                    if (v.y < 0) --y;
                    if (d < 1 && this[x, y] != 0) {
                        dist *= d;
                        v *= d;
                        if (v.y < 0) {
                            normal = XY.up;
                            pr = HorizontalEdgePrimitive.Up(++y);
                        } else {
                            normal = XY.down;
                            pr = HorizontalEdgePrimitive.Down(y);
                        }
                        break;
                    }
                    if (v.y > 0) ++y;
                }
            }
            if (width > 0) {
                // и теперь обойти вершины в тайлах.
                float d = 1;

                bp = beg;
                ep = bp + v;

                AABB aabb = new AABBF(
                    Mathf.Min(bp.x, ep.x) - width,
                    Mathf.Max(bp.x, ep.x) + width,
                    Mathf.Min(bp.y, ep.y) - width,
                    Mathf.Max(bp.y, ep.y) + width
                ).ToTiles(LandTile.size);

                for (int x = aabb.left; x < aabb.right; x++)
                for (int y = aabb.bottom; y < aabb.top; y++) {
                    foreach (XY pt in tiles[x, y].vertices) {
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

            if (v.x < 0) {
                points = new List<XY> {new XY(left, top), new XY(left, bottom)};
                if (v.y < 0) points.Add(new XY(right, bottom));
                else if (v.y > 0) points.Add(new XY(right, top));
            } else if (v.x > 0) {
                points = new List<XY> {new XY(right, top), new XY(right, bottom)};
                if (v.y < 0) points.Add(new XY(left, bottom));
                else if (v.y > 0) points.Add(new XY(left, top));
            } else {
                if (v.y < 0) points = new List<XY> {new XY(left, bottom), new XY(right, bottom)};
                else if (v.y > 0) points = new List<XY> {new XY(left, top), new XY(right, top)};
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
            XY normal = XY.zero;
            Primitive pr1 = null, pr2 = null;

            if (v.x != 0) {
                float xx = v.x < 0 ? left : right;
                XY a = new XY(xx, bottom);
                XY b = new XY(xx, top);
                XY n = v.x < 0 ? XY.right : XY.left;
                Primitive current = v.x < 0
                    ? VerticalEdgePrimitive.Left(xx)
                    : VerticalEdgePrimitive.Right(xx);

                AABB aabb = new AABBF(
                    xx + Mathf.Min(0, v.x),
                    xx + Mathf.Max(0, v.x),
                    Mathf.Min(a.y, b.y) + Mathf.Min(0, v.y),
                    Mathf.Max(a.y, b.y) + Mathf.Max(0, v.y)
                ).ToTiles(LandTile.size);

                for (int x = aabb.left; x < aabb.right; x++)
                for (int y = aabb.bottom; y < aabb.top; y++) {
                    foreach (XY pt in tiles[x, y].vertices) {
                        float d = Geom.CastRayToVertical(pt, -v, xx);
                        float yy = pt.y - v.y * d;
                        if (yy <= bottom || yy >= top || d >= dd) continue;
                        dd = d;
                        normal = n;
                        pr1 = current;
                        pr2 = CirclePrimitive.New(pt);
                    }
                }
            }

            if (v.y != 0) {
                float yy = v.y < 0 ? bottom : top;
                XY a = new XY(left, yy);
                XY b = new XY(right, yy);
                XY n = v.y < 0 ? XY.up : XY.down;
                Primitive current = v.y < 0
                    ? HorizontalEdgePrimitive.Down(yy)
                    : HorizontalEdgePrimitive.Up(yy);

                AABB aabb = new AABBF(
                    Mathf.Min(a.x, b.x) + Mathf.Min(0, v.x),
                    Mathf.Max(a.x, b.x) + Mathf.Max(0, v.x),
                    yy + Mathf.Min(0, v.y),
                    yy + Mathf.Max(0, v.y)
                ).ToTiles(LandTile.size);

                for (int x = aabb.left; x < aabb.right; x++)
                for (int y = aabb.bottom; y < aabb.top; y++) {
                    foreach (XY pt in tiles[x, y].vertices) {
                        float d = Geom.CastRayToHorizontal(pt, -v, yy);
                        float xx = pt.x - v.x * d;
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
