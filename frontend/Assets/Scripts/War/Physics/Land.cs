using UnityEngine;
using System;

namespace W3 {
    public class Land {
        int progress, fullProgress;

        byte[,] array;
        LandTiles tiles;
        int width, height; // do not make them odd
        int widthInTiles, heightInTiles;

        public float tangentialBounce, normalBounce;

        public Land (LandGen gen, Texture2D landTexture, SpriteRenderer renderer) {
            InitArray(gen);
            InitTiles();
            InitTexture(landTexture, renderer);
            tangentialBounce = 0.9f;
            normalBounce = 0.5f;
        }

        void InitArray(LandGen gen) {
            array = gen.array;

            width = array.GetLength(0);
            height = array.GetLength(1);
        }

        void InitTexture(Texture2D landTexture, SpriteRenderer renderer) {
            Texture2D tex = new Texture2D(width, height);
            for (int x = 0; x < width; ++x) {
                for (int y = 0; y < height; ++y) {
                    tex.SetPixel(x, y, array[x, y] == 0 ? Color.clear : landTexture.GetPixel(x & 0xff, y & 0xff));
                }
            }
            tex.Apply();
            renderer.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0, 0), 1f);
        }

        void InitTiles() {
            widthInTiles = width / LandTile.size;
            if (width % LandTile.size != 0) ++widthInTiles;
            heightInTiles = height / LandTile.size;
            if (height % LandTile.size != 0) ++heightInTiles;

            tiles = new LandTiles();
            for (int x = 0; x < widthInTiles; x++) {
                for (int y = 0; y < heightInTiles; y++) {
                    var tile = new LandTile(x, y);
                    tile.Recalculate(this);
                    tiles[x, y] = tile;
                }
            }
        }

        public byte this[int x, int y] {
            get { return (x < 0 || y < 0 || x >= width || y >= height) ? (byte)0 : array[x, y]; }
            // set { array[x, y] = value == 0 ? 0 : 1; }
        }

        //public Collision CastRay (XY origin, XY direction) {
        //    return null;
        //}

        public Collision CastRay (XY beg, XY end, float width) {
            XY bp, ep, offset, normal = XY.zero;
            Primitive pr = null;

            float dist = 1;
                
            int w = array.GetLength(0),
                h = array.GetLength(1);
            
            // луч проходит вправо, пересекая вертикали
            if (beg.x < end.x) {
                bp = beg + new XY(width, 0);
                ep = end + new XY(width, 0);
                //ep = bp * (1 - dist) + (end + new Pt2(width, 0)) * dist;
                for (
                    int x = Math.Max(0, Mathf.CeilToInt(bp.x)), eCol = Math.Min(w, Mathf.CeilToInt(ep.x));
                    x < eCol;
                    ++x
                ) {
                    float d = (x - bp.x) / (ep.x - bp.x);
                    int y = Mathf.FloorToInt(bp.y * (1 - d) + ep.y * d);
                    if (this[x, y] != 0) {
                        if(d < 1) {
                            dist = d;
                            normal = XY.left;
                            pr = VerticalEdgePrimitive.Left(x);
                        }
                        break;
                    }
                }
            }

            // луч проходит влево, пересекая вертикали
            if (beg.x > end.x) {
                bp = beg - new XY(width, 0);
                ep = end - new XY(width, 0);
                //ep = bp * (1 - dist) + (end - new Pt2(width, 0)) * dist;
                for (
                    int x = Math.Min(w, Mathf.FloorToInt(bp.x)), eCol = Math.Max(0, Mathf.FloorToInt(ep.x));
                    x > eCol;
                    --x
                ) {
                    float d = (x - bp.x) / (ep.x - bp.x);
                    int y = Mathf.FloorToInt(bp.y * (1 - d) + ep.y * d);
                    if(this[x-1, y] != 0) {
                        if (d < 1) {
                            dist = d;
                            normal = XY.right;
                            pr = VerticalEdgePrimitive.Right(x);
                        }
                    }
                }
            }

            // луч падает вниз, пересекая горизонтали
            // на самом деле вверх - это же юнити
            if (beg.y < end.y) {
                bp = beg + new XY(0, width);
                ep = bp * (1 - dist) + (end + new XY(0, width)) * dist;
                //ep = bp * (1 - dist) + (end + new Pt2(0, width)) * dist;

                for (
                    int y = Math.Max(0, Mathf.CeilToInt(bp.y)), eRow = Math.Min(h, Mathf.CeilToInt(ep.y));
                    y < eRow;
                    ++y
                ) {
                    float d = (y - bp.y) / (ep.y - bp.y);
                    int x = Mathf.FloorToInt(bp.x * (1 - d) + ep.x * d);
                    if(this[x, y] != 0) {
                        if(d < 1) {
                            dist *= d;
                            normal = XY.down;
                            pr = HorizontalEdgePrimitive.Down(y);
                        }
                    }
                }
            }

            // луч проходит вверх, пересекая горизонтали
            // (вниз)
            if (beg.y > end.y) {
                bp = beg - new XY(0, width);
                ep = bp * (1 - dist) + (end - new XY(0, width)) * dist;
                //ep = bp * (1 - dist) + (end - new Pt2(0, width)) * dist;

                for (
                    int y = Math.Min(h, Mathf.FloorToInt(bp.y)), eRow = Math.Max(0, Mathf.FloorToInt(ep.y));
                    y > eRow;
                    --y
                ) {
                    float d = (y - bp.y) / (ep.y - bp.y);
                    int x = Mathf.FloorToInt(bp.x * (1 - d) + ep.x * d);
                    if (this[x, y-1] != 0) {
                        if (d < 1) {
                            dist *= d;
                            normal = XY.up;
                            pr = HorizontalEdgePrimitive.Up(y);
                        }
                    }
                }
            }
            
            {
                // и теперь обойти вершины в тайлах.
                bp = beg;
                ep = beg * (1 - dist) + end * dist;
            
                int bCol = Mathf.FloorToInt(Math.Max(0,             Mathf.Min((bp.x - width) / LandTile.size, (ep.x - width) / LandTile.size)));
                int eCol = Mathf.CeilToInt (Math.Min(widthInTiles,  Mathf.Max((bp.x + width) / LandTile.size, (ep.x + width) / LandTile.size)));
                int bRow = Mathf.FloorToInt(Math.Max(0,             Mathf.Min((bp.y - width) / LandTile.size, (ep.y - width) / LandTile.size)));
                int eRow = Mathf.CeilToInt (Math.Min(heightInTiles, Mathf.Max((bp.y + width) / LandTile.size, (ep.y + width) / LandTile.size)));

                for (int x = bCol; x < eCol; x++) {
			        for (int y = bRow; y < eRow; y++) {
                        foreach (XY v in tiles[x, y].vertices) {
                            float d = Geom.CastRayToCircle(beg, end - beg, v, width);
                            if (d < dist) {
                                dist = d;
                                normal = beg*(1-d) + end*d - v;
                                pr = CirclePrimitive.New(v);
                            }
                        }
                    }
			    }
            }

            return dist < 1
                ? new Collision((end-beg)*dist, normal, null, null, CirclePrimitive.New(beg, width), pr)
                : null;
        }
    }
}