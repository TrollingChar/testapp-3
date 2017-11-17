using Battle.Physics.Collisions;

namespace Geometry
{
    public class Physics
    {
        public static bool Overlap(CircleCollider a, CircleCollider b)
        {
            float rr = a.Radius + b.Radius;
            return XY.SqrDistance(a.Center, b.Center) < rr * rr;
        }

        public static bool Overlap(CircleCollider c, BoxCollider b)
        {
            XY o = c.Center;
            float r = c.Radius;
            float top = b.Top;
            float left = b.Left;
            float right = b.Right;
            float bottom = b.Bottom;

            float r2 = r * r;
            
            if (left >= o.X + r || right <= o.X - r || bottom >= o.Y + r || top <= o.Y - r) return false;

            if (o.X >= right) {
                if (o.Y >= top)    return XY.SqrDistance(o, new XY(right, top)) < r2;
                if (o.Y <= bottom) return XY.SqrDistance(o, new XY(right, bottom)) < r2;
                return true;
            }
            if (o.X <= left) {
                if (o.Y >= top)    return XY.SqrDistance(o, new XY(left, top)) < r2;
                if (o.Y <= bottom) return XY.SqrDistance(o, new XY(left, bottom)) < r2;
                return true;
            }
            return true;
        }

        public static bool Overlap(BoxCollider a, BoxCollider b)
        {
            return a.Top > b.Bottom
                && a.Left < b.Right
                && a.Right > b.Left
                && a.Bottom < b.Top;
        }

        public static Collision FlyInto(CircleCollider a, CircleCollider b, XY v)
        {
            if (Overlap(a, b)) return null;
            var rayDist = Geom.RayToCircle(a.Center, v, b.Center, a.Radius + b.Radius);
            // todo: численными методами подобрать rayDist чтобы третий закон выполнялся
        }

        public static Collision FlyInto(CircleCollider c, BoxCollider b, XY v)
        {
            
        }

        public static Collision FlyInto(BoxCollider a, BoxCollider b, XY v)
        {
            
        }
    }
}