namespace Geometry
{
    public struct Box
    {
        public float Left, Right, Top, Bottom;

        public Box(float left, float right, float top, float bottom)
        {
            Top = top;
            Left = left;
            Right = right;
            Bottom = bottom;
        }
    }
}