using UnityEngine;


namespace Geometry {

    public struct XY {

        public float X, Y;


        static XY () {
            Zero = new XY(0f, 0f);
            One = new XY(1f, 1f);
            NaN = new XY(float.NaN, float.NaN);
            Left = new XY(-1f, 0f);
            Right = new XY(1f, 0f);
            Down = new XY(0f, -1f);
            Up = new XY(0f, 1f);
        }


        public XY (float x, float y) {
            X = x;
            Y = y;
        }


        public static XY operator - (XY a) {
            return new XY(-a.X, -a.Y);
        }


        public static XY operator - (XY a, XY b) {
            return new XY(a.X - b.X, a.Y - b.Y);
        }


        public static bool operator != (XY a, XY b) {
            return a.X != b.X || a.Y != b.Y;
        }


        public static XY operator * (float d, XY a) {
            return new XY(a.X * d, a.Y * d);
        }


        public static XY operator * (XY a, float d) {
            return new XY(a.X * d, a.Y * d);
        }


        public static XY operator / (XY a, float d) {
            return new XY(a.X / d, a.Y / d);
        }


        public static XY operator + (XY a, XY b) {
            return new XY(a.X + b.X, a.Y + b.Y);
        }


        public static bool operator == (XY a, XY b) {
            return a.X == b.X && a.Y == b.Y;
        }


        public static implicit operator XY (Vector2 v) {
            return new XY(v.x, v.y);
        }


        public static implicit operator Vector2 (XY v) {
            return new Vector2(v.X, v.Y);
        }


        public static XY Zero { get; private set; }
        public static XY One { get; private set; }
        public static XY Down { get; private set; }
        public static XY Left { get; private set; }
        public static XY Up { get; private set; }
        public static XY Right { get; private set; }
        public static XY NaN { get; private set; }

        public bool IsNaN {
            get { return float.IsNaN(X); }
        } // dont check Y

        public float Length {
            get { return Mathf.Sqrt(X * X + Y * Y); }
            set {
                float l = Length;
                if (l > 0) {
                    l = value / l;
                    X *= l;
                    Y *= l;
                }
                else Y = l;
            }
        }

        public float SqrLength {
            get { return X * X + Y * Y; }
        }

        public float Angle {
            get { return Mathf.Atan2(Y, X); }
            set {
                float l = Length;
                X = l * Mathf.Cos(value);
                Y = l * Mathf.Sin(value);
            }
        }


        public XY WithX (float x) {
            return new XY(x, Y);
        }


        public XY WithY (float y) {
            return new XY(X, y);
        }


        public XY WithLength (float l) {
            var v = this;
            v.Length = l;
            return v;
        }


        public void ClampLength (float l) {
            float len = Length;
            if (l >= len) return;
            l /= len;
            X *= l;
            Y *= l;
        }


        public XY WithLengthClamped (float l) {
            var v = this;
            v.ClampLength(l);
            return v;
        }


        public void ReduceLength (float delta) {
            float l = SqrLength;
            if (l > delta * delta) {
                Length -= delta;
            } else {
                X = Y = 0;
            }
        }


        public XY WithLengthReduced (float delta) {
            var v = this;
            v.ReduceLength(delta);
            return v;
        }


        public XY WithAngle (float a) {
            var v = this;
            v.Angle = a;
            return v;
        }


        public static float Dot (XY a, XY b) {
            return a.X * b.X + a.Y * b.Y;
        }


        public static float Cross (XY a, XY b) {
            return a.X * b.Y - a.Y * b.X;
        }


        public static XY Polar (float length, float angle) {
            return new XY(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);
        }


        public static float Distance (XY a, XY b) {
            return (b - a).Length;
        }


        public static float SqrDistance (XY a, XY b) {
            return (b - a).SqrLength;
        }


        public static float DirectionAngle (XY from, XY to) {
            return (to - from).Angle;
        }


        public void Normalize () {
            float l = Length;
            if (l > 0f) {
                X /= l;
                Y /= l;
            }
        }


        public XY Normalized () {
            var v = this;
            v.Normalize();
            return v;
        }


        public void Rotate (float angle) {
            this = Rotated(angle);
        }


        public void Rotate90CW () {
            float f = X;
            X = Y;
            Y = -f;
        }


        public void Rotate90CCW () {
            float f = X;
            X = -Y;
            Y = f;
        }


        public XY Rotated (float angle) {
            float sin = Mathf.Sin(angle);
            float cos = Mathf.Cos(angle);
            return new XY(X * cos - Y * sin, X * sin + Y * cos);
        }


        public XY Rotated90CW () {
            return new XY(Y, -X);
        }


        public XY Rotated90CCW () {
            return new XY(-Y, X);
        }


        public void Clamp (Box b) {
            X = Mathf.Clamp(X, b.Left, b.Right);
            Y = Mathf.Clamp(Y, b.Bottom, b.Top);
        }


        public XY Clamped (Box b) {
            return new XY(
                Mathf.Clamp(X, b.Left, b.Right),
                Mathf.Clamp(Y, b.Bottom, b.Top)
            );
        }


        public static XY Lerp (XY pos0, XY pos1, float t) {
            return new XY(
                pos0.X + (pos1.X - pos0.X) * t,
                pos0.Y + (pos1.Y - pos0.Y) * t
            );
        }


        public override string ToString () {
            return string.Format("({0:F1}, {1:F1})", X, Y);
        }


        public string ToString (string format) {
            return string.Format("({0}, {1})", X.ToString(format), Y.ToString(format));
        }


        public override bool Equals (object obj) {
            return obj is XY && this == (XY) obj;
        }


        public override int GetHashCode () {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

    }

}
