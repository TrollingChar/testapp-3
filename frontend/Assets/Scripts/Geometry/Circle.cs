namespace Geometry
{
    public struct Circle {
        public readonly XY Center;
        public readonly float Radius;

        public Circle(XY center, float radius) {
            Center = center;
            Radius = radius;
        }
    }
}