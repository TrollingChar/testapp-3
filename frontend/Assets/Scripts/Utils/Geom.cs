using System;
using UnityEngine;

namespace W3 {
    public static class Geom {
        public static XY Bounce (XY velocity, XY normal, float tangentialBounce, float normalBounce) {
            XY tangent = normal.Rotated90CW();
            XY convertedVelocity = ConvertToBasis(velocity, tangent, normal);
            //if (convertedVelocity == null) {
            //    Main.log("VECTOR CONVERSION ERROR");
            //    return new Pt2();
            //}
            return tangent * tangentialBounce * convertedVelocity.x - normal * normalBounce * convertedVelocity.y;
        }

        static XY ConvertToBasis (XY v, XY x, XY y) {
            // коэффициенты системы уравнений
            float
                a0 = x.x, b0 = y.x, c0 = v.x,
                a1 = x.y, b1 = y.y, c1 = v.y;
            // определитель матрицы при решении системы методом Крамера
            float d = a0 * b1 - a1 * b0;
            // null - прямые параллельны или совпадают
            return d == 0 ? XY.zero : new XY(c0 * b1 - c1 * b0, a0 * c1 - a1 * c0) / d;
        }

        public static float CastRayToCircle (XY origin, XY direction, XY circleCenter, float circleRadius) {
            XY originToCircle = circleCenter - origin;

            if (XY.Dot(direction, originToCircle) <= 0) return 1;//float.NaN;

            float sqrHeight = XY.Cross(direction, originToCircle);
            sqrHeight *= sqrHeight / direction.sqrLength;

            float sqrRadius = circleRadius * circleRadius;

            if(sqrHeight > sqrRadius) return 1;//float.NaN

            return Mathf.Clamp01((
                Mathf.Sqrt(originToCircle.sqrLength - sqrHeight)
                - Mathf.Sqrt(sqrRadius - sqrHeight)
            ) / direction.sqrLength);
        }
    }
}
