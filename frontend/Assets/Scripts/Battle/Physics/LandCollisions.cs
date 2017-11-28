using Battle.Physics.Collisions;
using Collisions;
using Geometry;
using UnityEngine;
using Primitive = Geometry.Primitive;


namespace Battle.Physics {

    public partial class Land {

        public NewCollision ApproxCollision (Circle c, XY v) {
            var primitive = Primitive.None;
            if (v.X > 0)      RayToTheRight (c.Center, ref v, c.Radius, ref primitive);
            if (v.X < 0)      RayToTheLeft  (c.Center, ref v, c.Radius, ref primitive);
            if (v.Y > 0)      RayToTheTop   (c.Center, ref v, c.Radius, ref primitive);
            if (v.Y < 0)      RayToTheBottom(c.Center, ref v, c.Radius, ref primitive);
            if (c.Radius > 0) RayToVertices (c.Center, ref v, c.Radius, ref primitive);
            return primitive.IsEmpty ? null : new NewCollision(v, Primitive.Circle(c), primitive);
        }


        private void RayToTheRight (XY o, ref XY v, float w, ref Primitive primitive) {
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
                return;
            }
        }


        private void RayToTheLeft (XY o, ref XY v, float w, ref Primitive primitive) {
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
                return;
            }
        }


        private void RayToTheTop (XY o, ref XY v, float w, ref Primitive primitive) {
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
                return;
            }
        }


        private void RayToTheBottom (XY o, ref XY v, float w, ref Primitive primitive) {
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
                return;
            }
        }


        private void RayToVertices (XY o, ref XY v, float r, ref Primitive primitive) {
            float d2 = v.SqrLength;
            XY nearestXY = o;

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
            if (nearestXY == o) return;
            
            primitive = Primitive.Circle(nearestXY);
            v.Length = Mathf.Sqrt(d2);
        }

    }

}
