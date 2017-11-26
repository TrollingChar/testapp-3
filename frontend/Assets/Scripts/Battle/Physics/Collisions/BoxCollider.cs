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

        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override Collision FlyInto (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
        }


        public override Collision FlyInto (CircleCollider c, XY velocity) {
            return -c.FlyInto(this, -velocity);
        }


        public override Collision FlyInto (BoxCollider c, XY velocity) {
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
            if (velocity.X < 0) {
                d = Geom.CastRayToVertical(new XY(left, bottom), velocity, cright);
                // спроецировать на прямую сразу две точки
                float topY = top + velocity.Y * d;
                float bottomY = bottom + velocity.Y * d;
                if (d < minDist && topY < cbottom && bottomY > ctop) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Right,
                        this,
                        c
                    );
                }
            }
            if (velocity.X > 0) {
                d = Geom.CastRayToVertical(new XY(right, top), velocity, cleft);
                float topY = top + velocity.Y * d;
                float bottomY = bottom + velocity.Y * d;
                if (d < minDist && topY < cbottom && bottomY > ctop) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Left,
                        this,
                        c
                    );
                }
            }
            if (velocity.Y < 0) {
                d = Geom.CastRayToHorizontal(new XY(left, bottom), velocity, ctop);
                float leftX = left + velocity.X * d;
                float rightX = right + velocity.X * d;
                if (d < minDist && rightX < cleft && leftX > cright) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Up,
                        this,
                        c
                    );
                }
            }
            if (velocity.Y > 0) {
                d = Geom.CastRayToVertical(new XY(right, top), velocity, cbottom);
                float leftX = left + velocity.X * d;
                float rightX = right + velocity.X * d;
                if (d < minDist && rightX < cleft && leftX > cright) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Down,
                        this,
                        c
                    );
                }
            }
            return result;
        }


        public override Collision FlyInto (Land land, XY velocity) {
//            var result = land.CastRectRay(Left, Right, Bottom, Top, velocity);
//            if (result != null) result.Collider1 = this;
//            return result;
            throw new NotImplementedException();
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
            return Geom.AreOverlapping(c.Center, c.Radius, Left, Right, Bottom, Top);
        }


        public override bool Overlaps (BoxCollider c) {
            return Left < c.Right
                && Right > c.Left
                && Bottom < c.Top
                && Top > c.Bottom;
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
