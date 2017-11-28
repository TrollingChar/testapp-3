using System;
using Core;


namespace Geometry {

    public static class PrimitiveUtils {

//        private delegate void OffsetGetter (Primitive a, Primitive b, ref XY offset);
//        private delegate bool OverlapDetector (Primitive a, Primitive b, XY offset);
//        private delegate XY NormalGetter (Primitive a, Primitive b);


        public static void GetOffsetNormal (Primitive a, Primitive b, ref XY offset, out XY normal) {
            if (Overlap(a, b, offset)) {
                float lo = 0;
                float hi = 1;
                float len = offset.Length;
                for (
                    int i = 0;
                    i < Settings.NumMethodsIterations && (hi - lo) * len > Settings.NumMethodsPrecision;
                    i++
                ) {
                    float mid = 0.5f * (lo + hi);
                    if (Overlap(a, b, mid * offset)) hi = mid;
                    else                             lo = mid;
                }
                offset *= lo;
            }
            normal = GetNormal(a, b, offset);
        }

/*
        private static bool OverlapCircleCircle (Primitive a, Primitive b, XY offset) {
            float rr = a.R + b.R;
            return XY.SqrDistance(new XY(a.X, a.Y) + offset, new XY(b.X, b.Y)) < rr * rr;
        }


        private static bool OverlapCircleLeft (Primitive a, Primitive b, XY offset) {
            return a.X + a.R + offset.X > b.X;
        }


        private static bool OverlapCircleRight (Primitive a, Primitive b, XY offset) {
            return a.X - a.R + offset.X < b.X;
        }


        private static bool OverlapCircleTop (Primitive a, Primitive b, XY offset) {
            return a.Y - a.R + offset.Y < b.Y;
        }


        private static bool OverlapCircleBottom (Primitive a, Primitive b, XY offset) {
            return a.Y + a.R + offset.Y > b.Y;
        }


        private static bool OverlapLeftCircle (Primitive a, Primitive b, XY offset) {
            return a.X + offset.X < b.X + b.R;
        }


        private static bool OverlapRightCircle (Primitive a, Primitive b, XY offset) {
            return a.X + offset.X > b.X - b.R;
        }


        private static bool OverlapTopCircle (Primitive a, Primitive b, XY offset) {
            return a.Y + offset.Y > b.Y - b.R;
        }


        private static bool OverlapBottomCircle (Primitive a, Primitive b, XY offset) {
            return a.Y + offset.Y < b.Y + b.R;
        }


        private static bool OverlapLeftRight (Primitive a, Primitive b, XY offset) {
            return a.X + offset.X < b.X;
        }


        private static bool OverlapRightLeft (Primitive a, Primitive b, XY offset) {
            return a.X + offset.X > b.X;
        }


        private static bool OverlapTopBottom (Primitive a, Primitive b, XY offset) {
            return a.Y + offset.Y > b.Y;
        }


        private static bool OverlapBottomTop (Primitive a, Primitive b, XY offset) {
            return a.Y + offset.Y < b.Y;
        }
*/

        private static bool Overlap (Primitive a, Primitive b, XY offset) {
            switch (a.Type) {
                case Primitive.PType.Circle:
                    switch (b.Type) {
                        case Primitive.PType.Circle:
                            float rr = a.R + b.R;
                            return XY.SqrDistance(new XY(a.X, a.Y) + offset, new XY(b.X, b.Y)) < rr * rr;
                        case Primitive.PType.Left:   return a.X + a.R + offset.X > b.X;
                        case Primitive.PType.Right:  return a.X - a.R + offset.X < b.X;
                        case Primitive.PType.Top:    return a.Y - a.R + offset.Y < b.Y;
                        case Primitive.PType.Bottom: return a.Y + a.R + offset.Y > b.Y;
                        default:                     return false;
                    }
                case Primitive.PType.Left:
                    switch (b.Type) {
                        case Primitive.PType.Circle: return a.X + offset.X < b.X + b.R;
                        case Primitive.PType.Right:  return a.X + offset.X < b.X;
                        default:                     return false;
                    }
                case Primitive.PType.Right:
                    switch (b.Type) {
                        case Primitive.PType.Circle: return a.X + offset.X > b.X - b.R;
                        case Primitive.PType.Left:   return a.X + offset.X > b.X;
                        default:                     return false;
                    }
                case Primitive.PType.Top:
                    switch (b.Type) {
                        case Primitive.PType.Circle: return a.Y + offset.Y > b.Y - b.R;
                        case Primitive.PType.Bottom: return a.Y + offset.Y > b.Y;
                        default:                     return false;
                    }
                case Primitive.PType.Bottom:
                    switch (b.Type) {
                        case Primitive.PType.Circle: return a.Y + offset.Y < b.Y + b.R;
                        case Primitive.PType.Top:    return a.Y + offset.Y < b.Y;
                        default:                     return false;
                    }
                default: return false;
            }
        }

        
        private static XY GetNormal (Primitive a, Primitive b, XY offset) {
            switch (a.Type) {
                case Primitive.PType.Circle:
                    switch (b.Type) {
                        case Primitive.PType.Circle: return new XY(a.X - b.X, a.Y - b.Y) + offset;
                        case Primitive.PType.Left:   return XY.Left;
                        case Primitive.PType.Right:  return XY.Right;
                        case Primitive.PType.Top:    return XY.Up;
                        case Primitive.PType.Bottom: return XY.Down;
                        default:                     return XY.NaN;
                    }
                case Primitive.PType.Left:   return XY.Right;
                case Primitive.PType.Right:  return XY.Left;
                case Primitive.PType.Top:    return XY.Down;
                case Primitive.PType.Bottom: return XY.Up;
                default:                     return XY.NaN;
            }
        }

    }

}
