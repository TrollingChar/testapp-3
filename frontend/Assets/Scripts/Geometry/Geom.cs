using UnityEngine;


namespace Geometry {

    public static class Geom {

        public static XY Bounce (XY velocity, XY normal, float tangentialBounce, float normalBounce) {
            XY tangent = normal.Rotated90CW();
            XY convertedVelocity = ConvertToBasis(velocity, tangent, normal);

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
            XY originToCircle = circleCenter - o;

            if (XY.Dot(dir, originToCircle) <= 0) return 1; //float.NaN;

            float h2 = XY.Cross(dir, originToCircle);
            h2 *= h2 / dir.SqrLength;

            float r2 = r * r;

            if (h2 > r2) return 1; //float.NaN

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

    }

}
