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


        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.CollideWithBox(this, -velocity);
        }


        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
            return -c.CollideWithBox(this, -velocity);
        }


        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
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
                        c,
                        VerticalEdgePrimitive.Left(left),
                        VerticalEdgePrimitive.Right(cright)
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
                        c,
                        VerticalEdgePrimitive.Right(right),
                        VerticalEdgePrimitive.Left(cleft)
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
                        c,
                        HorizontalEdgePrimitive.Down(bottom),
                        HorizontalEdgePrimitive.Up(ctop)
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
                        c,
                        HorizontalEdgePrimitive.Up(top),
                        HorizontalEdgePrimitive.Down(cbottom)
                    );
                }
            }
            return result;
        }


        public override Collision CollideWithLand (Land land, XY v) {
            var result = land.CastRectRay(Left, Right, Bottom, Top, v);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool OverlapsWith (Collider c) {
            return c.OverlapsWithBox(this);
        }


        public override bool OverlapsWithCircle (CircleCollider c) {
            return Geom.AreOverlapping(c.Center, c.Radius, Left, Right, Bottom, Top);
        }


        public override bool OverlapsWithBox (BoxCollider c) {
            return Left < c.Right
                && Right > c.Left
                && Bottom < c.Top
                && Top > c.Bottom;
        }


        public override bool OverlapsWithLand (Land land) {
            throw new NotImplementedException();
        }

    }

}
