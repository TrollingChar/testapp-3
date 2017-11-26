using System.Diagnostics.Tracing;


namespace Geometry {

    public struct Primitive {

        public enum PType : byte { None, Circle, Left, Right, Top, Bottom }


        public static readonly Primitive None = new Primitive(PType.None, float.NaN, float.NaN, float.NaN);
        public PType Type;
        public float X;
        public float Y;
        public float R;
        
        
        private Primitive (PType type, float x, float y, float r) {
            Type = type;
            X = x;
            Y = y;
            R = r;
        }


        public static Primitive Circle (float x, float y, float r = 0) {
            return new Primitive(PType.Circle, x, y, r);
        }


        public static Primitive Circle (XY xy, float r = 0) {
            return new Primitive(PType.Circle, xy.X, xy.Y, r);
        }


        public static Primitive Left (float x) {
            return new Primitive(PType.Left, x, float.NaN, float.NaN);
        }


        public static Primitive Right (float x) {
            return new Primitive(PType.Right, x, float.NaN, float.NaN);
        }


        public static Primitive Top (float y) {
            return new Primitive(PType.Top, float.NaN, y, float.NaN);
        }


        public static Primitive Bottom (float y) {
            return new Primitive(PType.Bottom, float.NaN, y, float.NaN);
        }

    }

}
