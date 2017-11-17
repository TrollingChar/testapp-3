using Battle.Physics.Collisions;
using UnityEngine;
using BoxCollider = Battle.Physics.Collisions.BoxCollider;
using Collision = Battle.Physics.Collisions.Collision;


namespace Geometry {

    public static class Physics {

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
            /*/ else
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


        public static Collision FlyInto (CircleCollider a, CircleCollider b, XY v) {}
        public static Collision FlyInto (CircleCollider c, BoxCollider b, XY v) {}
        public static Collision FlyInto (BoxCollider a, BoxCollider b, XY v) {}

    }

}
