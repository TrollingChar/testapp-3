using UnityEngine;


namespace Geometry {

    public static class Geom {

        public static XY Bounce (XY velocity, XY normal, float tangentialBounce, float normalBounce) {
            XY tangent = normal.Rotated90CW();
            XY convertedVelocity = ConvertToBasis(velocity, tangent, normal);

            return tangent * tangentialBounce * convertedVelocity.x - normal * normalBounce * convertedVelocity.y;
        }


        public static XY ConvertToBasis (XY v, XY x, XY y) {
            // коэффициенты системы уравнений
            float
                a0 = x.x, b0 = y.x, c0 = v.x,
                a1 = x.y, b1 = y.y, c1 = v.y;
            // определитель матрицы при решении системы методом Крамера
            float d = a0 * b1 - a1 * b0;
            // null - прямые параллельны или совпадают
            return d == 0 ? XY.NaN : new XY(c0 * b1 - c1 * b0, a0 * c1 - a1 * c0) / d;
        }


        public static float CastRayToCircle (XY o, XY dir, XY circleCenter, float r) {
            XY originToCircle = circleCenter - o;

            if (XY.Dot(dir, originToCircle) <= 0) return 1; //float.NaN;

            float h2 = XY.Cross(dir, originToCircle);
            h2 *= h2 / dir.sqrLength;

            float r2 = r * r;

            if (h2 > r2) return 1; //float.NaN

            return Mathf.Clamp01((Mathf.Sqrt(originToCircle.sqrLength - h2) - Mathf.Sqrt(r2 - h2)) / dir.length);
        }


        public static float CastRayToVertical (XY o, XY dir, float x) {
            if (dir.x == 0) return 1;
            float d = (x - o.x) / dir.x;
            return d < 0 || d > 1 ? 1 : d;
        }


        public static float CastRayToHorizontal (XY o, XY dir, float y) {
            if (dir.y == 0) return 1;
            float d = (y - o.y) / dir.y;
            return d < 0 || d > 1 ? 1 : d;
        }

    }

}
