using Battle.Physics.Collisions;
using UnityEngine;
using BoxCollider = Battle.Physics.Collisions.BoxCollider;
using Collider = Battle.Physics.Collisions.Collider;


namespace Geometry {

    // all of these must obey the 3 laws!
    public static class Physics {

        private const float Epsilon = 0.001f;
        private const float SqrEpsilon = Epsilon * Epsilon;


        public static bool Overlap (CircleCollider a, CircleCollider b) {
            float rr = a.Radius + b.Radius;
            return XY.SqrDistance(a.Center, b.Center) < rr * rr;
        }


        public static bool Overlap (CircleCollider c, BoxCollider b) {
            //* switch algorithm
            float closestX = Mathf.Clamp(c.Center.X, b.Left, b.Right);
            float closestY = Mathf.Clamp(c.Center.Y, b.Bottom, b.Top);

            float dx = c.Center.X - closestX;
            float dy = c.Center.Y - closestY;

            return dx * dx + dy * dy < c.Radius * c.Radius;
            /*/
            float halfW = 0.5f * (b.Right - b.Left);
            float halfH = 0.5f * (b.Top - b.Bottom);

            float distX = Mathf.Abs(c.Center.X - b.Left - halfW);
            float distY = Mathf.Abs(c.Center.Y - b.Bottom - halfH);

            float r = c.Radius;
            if (distX >= halfW + r || distY >= halfH + r) return false;
            if (distX < halfW || distY < halfH) return true;

            distX -= halfW;
            distY -= halfH;
            return distX * distX + distY * distY < r * r;
            //*/
        }


        public static bool Overlap (BoxCollider a, BoxCollider b) {
            return a.Top > b.Bottom
                && a.Left < b.Right
                && a.Right > b.Left
                && a.Bottom < b.Top;
        }


        public static NCollision FlyInto (CircleCollider a, CircleCollider b, XY v) {
            float dist = Geom.RayToCircle(a.Center, v, b.Center, a.Radius + b.Radius);
            if (float.IsNaN(dist) || dist < 0 || dist * dist > v.SqrLength) {
                // no collision
                dist = v.Length;
            }

            var ao = a.Center;
            var bo = b.Center;
            float r2 = a.Radius + b.Radius;
            r2 *= r2;

            // проверим значение из формулы
            if (XY.SqrDistance(ao + v, bo) < r2) return new NCollision(v, XY.NaN, null, null);

            float lo = 0;
            float hi = dist;

            // численными методами делаем чтобы не было ошибки с перекрыванием
            for (int i = 0; i < 10 && hi - lo > Epsilon; i++) {
                float mid = 0.5f * (lo + hi);
                if (XY.SqrDistance(ao + v.WithLength(mid), bo) < r2) {
                    lo = mid;
                } else {
                    hi = mid;
                }
            }
            var offset = v.WithLength(lo);
            return new NCollision(offset, ao - bo + offset, a, b);
        }


        public static NCollision FlyInto (CircleCollider c, BoxCollider b, XY v) {
            // сначала стороны, потом углы, потом численными методами доделать
            float min = 1;
            if (v.X > 0) {
                float d = Geom.ORayToVertical(left, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) {
                    min = d;
                }
            }
            if (v.X < 0) {
                float d = Geom.ORayToVertical(right, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) {
                    min = d;
                }
            }
            if (v.Y > 0) {
                float d = Geom.ORayToHorizontal(bottom, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) {
                    min = d;
                }
            }
            if (v.Y < 0) {
                float d = Geom.ORayToHorizontal(top, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) {
                    min = d;
                }
            }
            if (v.X > 0 || v.Y > 0) {
                // bottom left
            }
            if (v.X < 0 || v.Y > 0) {
                // bottom right
            }
            if (v.X < 0 || v.Y < 0) {
                // top right
            }
            if (v.X > 0 || v.Y < 0) {
                // top left
            }
            
            // todo: find closest point using clamp, then compute normal vector
        }


        public static NCollision FlyInto (BoxCollider a, BoxCollider b, XY v) {
            // todo: optimize formulas
            float hw = 0.5f * (a.Right - a.Left + b.Right - b.Left);
            float hh = 0.5f * (a.Top - a.Bottom + b.Top - b.Bottom);
            float dx = 0.5f * (b.Left - a.Left + b.Right - a.Right);
            float dy = 0.5f * (b.Bottom - a.Bottom + b.Top - a.Top);

            float top = dy + hh;
            float left = dx - hw;
            float right = dx + hw;
            float bottom = dy - hh;

            float min = 1;
            XY normal = XY.NaN;
            if (v.X > 0) {
                float d = Geom.ORayToVertical(left, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) {
                    min = d;
                    normal = XY.Left;
                }
            }
            if (v.X < 0) {
                float d = Geom.ORayToVertical(right, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) {
                    min = d;
                    normal = XY.Right;
                }
            }
            if (v.Y > 0) {
                float d = Geom.ORayToHorizontal(bottom, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) {
                    min = d;
                    normal = XY.Down;
                }
            }
            if (v.Y < 0) {
                float d = Geom.ORayToHorizontal(top, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) {
                    min = d;
                    normal = XY.Up;
                }
            }

            if (v.Y + a.Top > b.Bottom &&
                v.X + a.Left < b.Right &&
                v.X + a.Right > b.Left &&
                v.Y + a.Bottom < b.Top) {
                return new NCollision(v, XY.NaN, null, null);
            }

            // включаем численные методы
            float lo = 0;
            float hi = min;
            float l = v.Length;
            for (int i = 0; i < 10 && l * (hi - lo) > Epsilon; i++) {
                float mid = 0.5f * (lo + hi);
                var offset = v * mid;
                if (offset.Y + a.Top > b.Bottom &&
                    offset.X + a.Left < b.Right &&
                    offset.X + a.Right > b.Left &&
                    offset.Y + a.Bottom < b.Top) {
                    hi = mid;
                } else {
                    lo = mid;
                }
            }

            if (normal.IsNaN) {
                var center = v * lo;
                var otherCenter = new XY(dx, dy);
                var line = otherCenter - center;
                if (line.X > 0) {
                    normal = Geom.LineIntersectsSegment(center, otherCenter, new XY(left, bottom), new XY(left, top))
                        ? XY.Left
                        : new XY(0, -Mathf.Sign(line.Y));
                }
            }
            return new NCollision(v * lo, normal, a, b);
        }

    }


    public struct NCollision {

        public readonly XY Offset;
        public readonly XY Normal;
        public readonly Collider C1;
        public readonly Collider C2;


        public NCollision (XY offset, XY normal, Collider c1, Collider c2) {
            Offset = offset;
            Normal = normal;
            C1 = c1;
            C2 = c2;
        }


        public static NCollision operator - (NCollision c) {
            return new NCollision(-c.Offset, -c.Normal, c.C2, c.C1);
        }

    }

}
