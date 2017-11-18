using Battle.Physics.Collisions;
using UnityEngine.Rendering;
using Mathf = UnityEngine.Mathf;


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
            // todo
        }


        public static NCollision FlyInto (BoxCollider a, BoxCollider b, XY v) {
            // todo
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
