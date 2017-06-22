using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public struct XY {
        public float x, y;

        public XY (float x, float y) {
            this.x = x;
            this.y = y;
        }

        public static XY operator - (XY a) {
            return new XY(-a.x, -a.y);
        }
        public static XY operator - (XY a, XY b) {
            return new XY(a.x - b.x, a.y - b.y);
        }
        public static bool operator != (XY a, XY b) {
            return a.x != b.x || a.y != b.y;
        }
        public static XY operator * (float d, XY a) {
            return new XY(a.x * d, a.y * d);
        }
        public static XY operator * (XY a, float d) {
            return new XY(a.x * d, a.y * d);
        }
        public static XY operator / (XY a, float d) {
            return new XY(a.x / d, a.y / d);
        }
        public static XY operator + (XY a, XY b) {
            return new XY(a.x + b.x, a.y + b.y);
        }
        public static bool operator == (XY a, XY b) {
            return a.x == b.x && a.y == b.y;
        }
        public static implicit operator XY (Vector2 v) {
            return new XY(v.x, v.y);
        }
        public static implicit operator Vector2 (XY v) {
            return new Vector2(v.x, v.y);
        }

        public static XY zero { get { return new XY(0f, 0f); } }
        public static XY one { get { return new XY(1f, 1f); } }
        public static XY down { get { return new XY(0f, -1f); } }
        public static XY left { get { return new XY(-1f, 0f); } }
        public static XY up { get { return new XY(0f, 1f); } }
        public static XY right { get { return new XY(1f, 0f); } }
        public static XY NaN { get { return new XY(float.NaN, float.NaN); } }
        public bool isNaN { get { return float.IsNaN(x); } } // dont check Y
        public float length {
            get { return Mathf.Sqrt(x * x + y * y); }
            set {
                float l = length;
                if (l > 0) {
                    l = value / l;
                    x *= l;
                    y *= l;
                } else y = l;
            }
        }
        public float sqrLength {
            get { return x * x + y * y; }
        }
        public float angle {
            get { return Mathf.Atan2(y, x); }
            set {
                float l = length;
                x = l * Mathf.Cos(value);
                y = l * Mathf.Sin(value);
            }
        }

        public XY WithX (float x) {
            return new XY(x, y);
        }
        public XY WithY (float y) {
            return new XY(x, y);
        }
        public XY WithLength (float l) {
            XY v = this;
            v.length = l;
            return v;
        }
        public void ClampLength (float l) {
            float len = length;
            if (l >= len) return;
            l /= len;
            x *= l;
            y *= l;
        }
        public XY WithLengthClamped (float l) {
            XY v = this;
            v.ClampLength(l);
            return v;
        }
        public void ReduceLength (float delta) {
            float l = sqrLength;
            if (l > delta * delta) length -= delta;
            else x = y = 0;
        }
        public XY WithLengthReduced (float delta) {
            XY v = this;
            v.ReduceLength(delta);
            return v;
        }
        public XY WithAngle (float a) {
            XY v = this;
            v.angle = a;
            return v;
        }

        public static float Dot (XY a, XY b) {
            return a.x * b.x + a.y * b.y;
        }
        public static float Cross (XY a, XY b) {
            return a.x * b.y - a.y * b.x;
        }

        public static XY FromPolar (float length, float angle) {
            return new XY(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);
        }

        public static float Distance (XY a, XY b) {
            return (b - a).length;
        }
        public static float SqrDistance (XY a, XY b) {
            return (b - a).sqrLength;
        }
        public static float Angle (XY from, XY to) {
            return (to - from).angle;
        }

        public void Normalize () {
            float l = length;
            if (l > 0) {
                x /= l;
                y /= l;
            }
        }
        public XY Normalized () {
            XY v = this;
            v.Normalize();
            return v;
        }

        public void Rotate (float angle) {
            this = Rotated(angle);
        }
        public void Rotate90CW () {
            float f = x;
            x = y;
            y = -f;
        }
        public void Rotate90CCW () {
            float f = x;
            x = -y;
            y = f;
        }
        public XY Rotated (float angle) {
            float sin = Mathf.Sin(angle), cos = Mathf.Cos(angle);
            return new XY(x * cos - y * sin, x * sin + y * cos);
        }
        public XY Rotated90CW () {
            return new XY(y, -x);
        }
        public XY Rotated90CCW () {
            return new XY(-y, x);
        }

        public static XY Lerp (XY pos0, XY pos1, float t) {
            return new XY(pos0.x + (pos1.x - pos0.x) * t,
                          pos0.y + (pos1.y - pos0.y) * t);
        }

        public override string ToString () {
            return string.Format("({0:F1}, {1:F1})", x, y);
        }
        public string ToString (string format) {
            return string.Format("({0}, {1})", x.ToString(format), y.ToString(format));
        }

        public override bool Equals (object obj) {
            if (!(obj is XY)) return false;
            XY v = (XY)obj;
            return this == v;
        }
        public override int GetHashCode () {
            return x.GetHashCode() ^ y.GetHashCode() << 2;
        }

    }
}
