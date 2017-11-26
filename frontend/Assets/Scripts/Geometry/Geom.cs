using UnityEngine;


namespace Geometry {

    public static class Geom {

        // вместо x можно y и норм
        public static float RayTo1D (float oX, float dirX, float targX) {
            return (targX - oX) / dirX;
        }


        // возвращает расстояние, пройденное лучом, может вернуть отрицательное или nan
        public static float RayToCircle (XY o, XY v, XY cCenter, float r) {
            var oc = cCenter - o;
            // если луч направлен в другую сторону
            if (XY.Dot(oc, v) <= 0) return float.NaN;
            // квадрат расстояния от центра окружности до луча
            float h2 = XY.Cross(oc, v);
            h2 *= h2 / v.SqrLength;

            return Mathf.Sqrt(oc.SqrLength - h2) - Mathf.Sqrt(r * r - h2);
        }

    }

}
