using System;
using Geometry;


namespace Battle.Physics.Collisions {

    public class BoxCollider : Collider {

        private readonly float _leftOffset;
        private readonly float _rightOffset;
        private readonly float _bottomOffset;
        private readonly float _topOffset;


        public BoxCollider (
            float left,
            float right,
            float bottom,
            float top
        ) {
            _leftOffset = left;
            _rightOffset = right;
            _bottomOffset = bottom;
            _topOffset = top;
        }


        public float Left {
            get { return _leftOffset + Object.Position.X; }
        }

        public float Right {
            get { return _rightOffset + Object.Position.X; }
        }

        public float Bottom {
            get { return _bottomOffset + Object.Position.Y; }
        }

        public float Top {
            get { return _topOffset + Object.Position.Y; }
        }

        public Box Box {
            get { return new Box(Left, Right, Top, Bottom); }
        }

        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override Collision FlyInto (Collider c, XY v) {
            return v == XY.Zero
                ? new Collision(v, XY.NaN, null, null)
                : -c.FlyInto(this, -v);
        }


        public override Collision FlyInto (CircleCollider c, XY v) {
//            return -c.FlyInto(this, -v);
        }


        public override Collision FlyInto (BoxCollider c, XY v) {
            /*
            float
                left = Left,
                right = Right,
                bottom = Bottom,
                top = Top,
                cleft = c.Left,
                cright = c.Right,
                cbottom = c.Bottom,
                ctop = c.Top;

            float minDist = 1;
            Collision result = null;

            float d;
            if (v.X < 0) {
                d = Geom_.CastRayToVertical(new XY(left, bottom), v, cright);
                // спроецировать на прямую сразу две точки
                float topY = top + v.Y * d;
                float bottomY = bottom + v.Y * d;
                if (d < minDist && topY < cbottom && bottomY > ctop) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Right,
                        this,
                        c
                    );
                }
            }
            if (v.X > 0) {
                d = Geom_.CastRayToVertical(new XY(right, top), v, cleft);
                float topY = top + v.Y * d;
                float bottomY = bottom + v.Y * d;
                if (d < minDist && topY < cbottom && bottomY > ctop) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Left,
                        this,
                        c
                    );
                }
            }
            if (v.Y < 0) {
                d = Geom_.CastRayToHorizontal(new XY(left, bottom), v, ctop);
                float leftX = left + v.X * d;
                float rightX = right + v.X * d;
                if (d < minDist && rightX < cleft && leftX > cright) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Up,
                        this,
                        c
                    );
                }
            }
            if (v.Y > 0) {
                d = Geom_.CastRayToVertical(new XY(right, top), v, cbottom);
                float leftX = left + v.X * d;
                float rightX = right + v.X * d;
                if (d < minDist && rightX < cleft && leftX > cright) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Down,
                        this,
                        c
                    );
                }
            }
            return result;
            */
        }


        public override Collision FlyInto (Land land, XY v) {
            var result = land.CastRectRay(Left, Right, Bottom, Top, v);
            if (!result.IsEmpty) result.Collider1 = this;
            return result;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
//            return Geom_.AreOverlapping(c.Center, c.Radius, Left, Right, Bottom, Top);
        }


        public override bool Overlaps (BoxCollider c) {
//            return Left < c.Right
//                && Right > c.Left
//                && Bottom < c.Top
//                && Top > c.Bottom;
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Box: left:" + Left.ToString("R") + ", right:" + Right.ToString("R")
                + ", bottom:" + Bottom.ToString("R") + ", top:" + Top.ToString("R");
        }

    }

}
