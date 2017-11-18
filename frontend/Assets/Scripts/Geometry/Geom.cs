using UnityEngine;


namespace Geometry {

    public class Geom {

        // returns distance or NaN
        public static float RayToCircle (XY origin, XY dir, XY circleCenter, float r) {
            var oc = circleCenter - origin;

            // если луч направлен в другую сторону
            if (XY.Dot(dir, oc) <= 0) return float.NaN;

            float s = XY.Cross(dir, oc);
            // квадрат расстояния от центра окружности до луча
            float h2 = s * s / dir.SqrLength;

            float r2 = r * r;
            // if (h2 > r2) return NaN;  -- sqrt от отрицательного числа и так NaN

            // да, тут может вернуться отрицательное число
            return Mathf.Sqrt(oc.SqrLength - h2) - Mathf.Sqrt(r2 - h2);
        }


        // return relative distance or NaN
        public static float RayToVertical (XY origin, XY dir, float x) {
            return dir.X == 0 ? float.NaN : (x - origin.X) / dir.X;
        }


        public static float RayToHorizontal (XY origin, XY dir, float y) {
            return dir.Y == 0 ? float.NaN : (y - origin.Y) / dir.Y;
        }

    }

}
