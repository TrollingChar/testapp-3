using System.Linq;
using Battle.Physics.Collisions;
using Collisions;
using Geometry;
using UnityEngine;
using Primitive = Geometry.Primitive;


namespace Battle.Physics {

    public partial class Land {

        public NewCollision ApproxCollision (Circle c, XY v) {
            var primitive = Primitive.None;
            bool collided = false;
            if (v.X > 0)      collided |= RayToTheRight (c.Center, ref v, c.Radius, ref primitive);
            if (v.X < 0)      collided |= RayToTheLeft  (c.Center, ref v, c.Radius, ref primitive);
            if (v.Y > 0)      collided |= RayToTheTop   (c.Center, ref v, c.Radius, ref primitive);
            if (v.Y < 0)      collided |= RayToTheBottom(c.Center, ref v, c.Radius, ref primitive);
            if (c.Radius > 0) collided |= RayToVertices (c.Center, ref v, c.Radius, ref primitive);
            return collided ? null : new NewCollision(v, Primitive.Circle(c), primitive);
        }


        public NewCollision ApproxCollision (Box b, XY v) {
            var boxPrimitive = Primitive.None;
            var landPrimitive = Primitive.None;
            bool collided = false;
            // сначала надо разобраться с вершинами прямоугольника летящими в землю
            
            if (v.X > 0 || v.Y > 0) {
                var corner = new XY(b.Right, b.Top);
                if (RayToTheRight(corner, ref v, 0, ref landPrimitive) |
                    RayToTheTop  (corner, ref v, 0, ref landPrimitive)) {
                    collided = true;
                    boxPrimitive = Primitive.Circle(corner);
                }
            }
            if (v.X > 0 || v.Y < 0) {
                var corner = new XY(b.Right, b.Bottom);
                if (RayToTheRight (corner, ref v, 0, ref landPrimitive) |
                    RayToTheBottom(corner, ref v, 0, ref landPrimitive)) {
                    collided = true;
                    boxPrimitive = Primitive.Circle(corner);
                }
            }
            if (v.X < 0 || v.Y < 0) {
                var corner = new XY(b.Left, b.Bottom);
                if (RayToTheLeft  (corner, ref v, 0, ref landPrimitive) |
                    RayToTheBottom(corner, ref v, 0, ref landPrimitive)) {
                    collided = true;
                    boxPrimitive = Primitive.Circle(corner);
                }
            }
            if (v.X < 0 || v.Y > 0) {
                var corner = new XY(b.Left, b.Top);
                if (RayToTheLeft(corner, ref v, 0, ref landPrimitive) |
                    RayToTheTop (corner, ref v, 0, ref landPrimitive)) {
                    collided = true;
                    boxPrimitive = Primitive.Circle(corner);
                }
            }
            
            // потом с сторонами прямоугольника и вершинами земли
            if (v.X > 0) {
                var aabb = new AABBF(
                    b.Right,
                    b.Right + v.X,
                    Mathf.Min(0, v.X) + b.Bottom,
                    Mathf.Max(0, v.X) + b.Top
                ).ToTiles(LandTile.Size);
                
                float d2 = v.SqrLength;
                XY nearestXY = XY.NaN;
                
                for (int x = aabb.Left; x < aabb.Right; x++)
                for (int y = aabb.Bottom; y < aabb.Top; y++) {
                    foreach (var pt in Tiles[x, y].Vertices) {
                        if (pt.X < b.Right || pt.X >= b.Right + v.X) continue;
                        float dist = Geom.RayTo1D(b.Right, v.X, pt.X);

                        float yy = pt.X - v.Y * dist;
                        if (yy < b.Bottom || yy > b.Top) continue;
                        
                        d2 = dist * dist;
                        nearestXY = pt;
                    }
                }
                if (nearestXY.IsNaN) return false; // no collision
            
                boxPrimitive  = Primitive.Right(b.Right);
                landPrimitive = Primitive.Circle(nearestXY);
                v.Length = Mathf.Sqrt(d2);
            }; // vertices to right side
            
            if (v.X < 0) ; // vertices to left side
            if (v.Y < 0) ; // vertices to bottom side
            if (v.Y > 0) ; // vertices to top side
            return boxPrimitive.IsEmpty ? null : new NewCollision(v, boxPrimitive, landPrimitive);
        }


        private bool RayToTheRight (XY o, ref XY v, float w, ref Primitive primitive) {
            var startXY = new XY(o.X + w, o.Y);
            var endXY = startXY + v;

            int startX = Mathf.Clamp(Mathf.CeilToInt(startXY.X), 0, Width);
            int endX = Mathf.Clamp(Mathf.CeilToInt(endXY.X), 0, Width);
            for (int x = startX; x < endX; x++) {
                float d = Geom.RayTo1D(startXY.X, v.X, x);
                int y = Mathf.FloorToInt(startXY.Y + d * v.Y);
                if (this[x, y] == 0) continue;
                v *= d;
                primitive = Primitive.Left(x);
                return true;
            }
            return false;
        }


        private bool RayToTheLeft (XY o, ref XY v, float w, ref Primitive primitive) {
            var startXY = new XY(o.X - w, o.Y);
            var endXY = startXY + v;

            int startX = Mathf.Clamp(Mathf.FloorToInt(startXY.X) - 1, 0, Width);
            int endX = Mathf.Clamp(Mathf.FloorToInt(endXY.X), 0, Width);
            for (int x = startX; x >= endX; x--) {
                float d = Geom.RayTo1D(startXY.X, v.X, x + 1);
                int y = Mathf.FloorToInt(startXY.Y + d * v.Y);
                if (this[x, y] == 0) continue;
                v *= d;
                primitive = Primitive.Right(x + 1);
                return true;
            }
            return false;
        }


        private bool RayToTheTop (XY o, ref XY v, float w, ref Primitive primitive) {
            var startXY = new XY(o.X, o.Y + w);
            var endXY = startXY + v;

            int startY = Mathf.Clamp(Mathf.CeilToInt(startXY.Y), 0, Height);
            int endY = Mathf.Clamp(Mathf.CeilToInt(endXY.Y), 0, Height);
            for (int y = startY; y < endY; y++) {
                float d = Geom.RayTo1D(startXY.Y, v.Y, y);
                int x = Mathf.FloorToInt(startXY.X + d * v.X);
                if (this[x, y] == 0) continue;
                v *= d;
                primitive = Primitive.Bottom(y);
                return true;
            }
            return false;
        }


        private bool RayToTheBottom (XY o, ref XY v, float w, ref Primitive primitive) {
            var startXY = new XY(o.X, o.Y - w);
            var endXY = startXY + v;

            int startY = Mathf.Clamp(Mathf.FloorToInt(startXY.Y) - 1, 0, Height);
            int endY = Mathf.Clamp(Mathf.FloorToInt(endXY.Y), 0, Height);
            for (int y = startY; y >= endY; y--) {
                float d = Geom.RayTo1D(startXY.Y, v.Y, y + 1);
                int x = Mathf.FloorToInt(startXY.X + d * v.X);
                if (this[x, y] == 0) continue;
                v *= d;
                primitive = Primitive.Top(y + 1);
                return true;
            }
            return false;
        }


        private bool RayToVertices (XY o, ref XY v, float r, ref Primitive primitive) {
            float d2 = v.SqrLength;
            XY nearestXY = XY.NaN;

            var aabb = new AABBF(
                Mathf.Min(o.X, o.X + v.X) - r,
                Mathf.Max(o.X, o.X + v.X) + r,
                Mathf.Min(o.Y, o.Y + v.Y) - r,
                Mathf.Max(o.Y, o.Y + v.Y) + r
            ).ToTiles(LandTile.Size);

            for (int x = aabb.Left; x < aabb.Right; x++)
            for (int y = aabb.Bottom; y < aabb.Top; y++) {
                foreach (var pt in Tiles[x, y].Vertices) {
                    float dist = Geom.RayToCircle(o, v, pt, r);
                    if (float.IsNaN(dist) || dist < 0 || dist * dist >= d2) continue;
                    d2 = dist * dist;
                    nearestXY = pt;
                }
            }
            if (nearestXY.IsNaN) return false;
            
            primitive = Primitive.Circle(nearestXY);
            v.Length = Mathf.Sqrt(d2);

            return true;
        }

    }

}
