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
        public static float RayToVertical (float originX, float dirX, float x) {
            return dirX == 0 ? float.NaN : (x - originX) / dirX;
        }


        public static float RayToHorizontal (float originY, float dirY, float y) {
            return dirY == 0 ? float.NaN : (y - originY) / dirY;
        }
        
        
        public static float ORayToVertical (float dirX, float x) {
            return dirX == 0 ? float.NaN : x / dirX;
        }


        public static float ORayToHorizontal (float dirY, float y) {
            return dirY == 0 ? float.NaN : y / dirY;
        }


        public static bool LineIntersectsSegment (XY la, XY lb, XY sa, XY sb) {
            var line = la - lb;
            return XY.Cross(line, sa - la) * XY.Cross(line, sb - lb) < 0;
        }


        public static XY BoxQuarter(XY point, float left, float right, float bottom, float top)
        {
            bool belowMainDiagonal = XY.Cross(new XY(right - left, top - bottom), point - new XY(left, bottom)) < 0;
            bool belowSecondDiagonal = XY.Cross(new XY(right - left, bottom - top), point - new XY(left, top)) < 0;
            
            return belowMainDiagonal
                ? (belowSecondDiagonal ? XY.Down : XY.Right)
                : (belowSecondDiagonal ? XY.Left : XY.Up);
        }


        public static float SqrDistance(XY point, Box box) {
            return XY.SqrDistance(point, point.Clamped(box));
        }


        public static float Distance(XY point, Box box) {
            return Mathf.Sqrt(SqrDistance(point, box));
        }

    }

}
