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
            
            // todo:
            // если лежит в прямоугольнике - вернуть true
            // 
            
            // если окружность полностью внутри прямоугольника
            if (o.X > left && o.X < right && o.Y > bottom && o.Y < top) return true;

            float r2 = r * r;
            // стороны
            return top > o.Y - r
                && left < o.X + r
                && right > o.X - r
                && bottom < o.Y + r
            /* BUG!!! -
                 ____
                |    |
                |    |
               /|____|\
               \_/ .\_/
                   |_______if point is circle center then algorithm returns false instead of true!
            */
            // вершины
                && XY.SqrDistance(o, new XY(left, bottom)) < r2
                && XY.SqrDistance(o, new XY(left, top)) < r2
                && XY.SqrDistance(o, new XY(right, bottom)) < r2
                && XY.SqrDistance(o, new XY(right, top)) < r2;
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