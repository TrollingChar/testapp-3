using System;
using UnityEngine;


namespace Geometry {

    [Obsolete]
    public static class OldGeom {

        public static XY Bounce (XY velocity, XY normal, float tangentialBounce, float normalBounce) {
            var tangent = normal.Rotated90CW();
            var convertedVelocity = ConvertToBasis(velocity, tangent, normal);

            return tangent * tangentialBounce * convertedVelocity.X - normal * normalBounce * convertedVelocity.Y;
        }


        public static XY ConvertToBasis (XY v, XY x, XY y) {
            // коэффициенты системы уравнений
            float
                a0 = x.X, b0 = y.X, c0 = v.X,
                a1 = x.Y, b1 = y.Y, c1 = v.Y;
            // определитель матрицы при решении системы методом Крамера
            float d = a0 * b1 - a1 * b0;
            // null - прямые параллельны или совпадают
            return d == 0 ? XY.NaN : new XY(c0 * b1 - c1 * b0, a0 * c1 - a1 * c0) / d;
        }


        public static float CastRayToCircle (XY o, XY dir, XY circleCenter, float r) {
            var originToCircle = circleCenter - o;
            // если луч направлен в другую сторону
            if (XY.Dot(dir, originToCircle) <= 0) return 1; //float.NaN;
            // квадрат расстояния от центра окружности до луча
            float h2 = XY.Cross(dir, originToCircle);
            h2 *= h2 / dir.SqrLength;

            float r2 = r * r;
            if (h2 > r2) return 1; //float.NaN
            // да, тут может вернуться отрицательное число, поэтому Clamp
            return Mathf.Clamp01((Mathf.Sqrt(originToCircle.SqrLength - h2) - Mathf.Sqrt(r2 - h2)) / dir.Length);
        }


        public static float CastRayToVertical (XY o, XY dir, float x) {
            if (dir.X == 0) return 1;
            float d = (x - o.X) / dir.X;
            return d < 0 || d > 1 ? 1 : d;
        }


        public static float CastRayToHorizontal (XY o, XY dir, float y) {
            if (dir.Y == 0) return 1;
            float d = (y - o.Y) / dir.Y;
            return d < 0 || d > 1 ? 1 : d;
        }


        public static bool AreOverlapping (
            XY circleCenter,
            float circleRadius,
            float boxLeft,
            float boxRight,
            float boxBottom,
            float boxTop
        ) {
            XY closestPoint = new XY(
                Mathf.Clamp(circleCenter.X, boxLeft, boxRight),
                Mathf.Clamp(circleCenter.Y, boxBottom, boxTop)
            );
            return XY.SqrDistance(circleCenter, closestPoint) < circleRadius * circleRadius;
            
            // если окружность полностью внутри прямоугольника
            if (circleCenter.X > boxLeft && circleCenter.X < boxRight &&
                circleCenter.Y > boxBottom && circleCenter.Y < boxTop) {
                return true;
            }

            float sqrRadius = circleRadius * circleRadius;
            // стороны
            return boxLeft < circleCenter.X + circleRadius
                && boxRight > circleCenter.X - circleRadius
                && boxBottom < circleCenter.Y + circleRadius
                && boxTop > circleCenter.Y - circleRadius
                // вершины
                && XY.SqrDistance(circleCenter, new XY(boxLeft, boxBottom)) < sqrRadius
                && XY.SqrDistance(circleCenter, new XY(boxLeft, boxTop)) < sqrRadius
                && XY.SqrDistance(circleCenter, new XY(boxRight, boxBottom)) < sqrRadius
                && XY.SqrDistance(circleCenter, new XY(boxRight, boxTop)) < sqrRadius;
        }

    }

}
