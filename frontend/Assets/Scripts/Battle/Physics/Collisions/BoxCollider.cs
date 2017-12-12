using System;
using Collisions;
using Geometry;
using UnityEngine;


namespace Battle.Physics.Collisions {

    public class BoxCollider : Collider {

        private readonly float _leftOffset;
        private readonly float _rightOffset;
        private readonly float _bottomOffset;
        private readonly float _topOffset;


        public BoxCollider (float left, float right, float bottom, float top) {
            _topOffset = top;
            _leftOffset = left;
            _rightOffset = right;
            _bottomOffset = bottom;
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
            get { return new Box(Left, Right, Bottom, Top); }
        }


        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override NewCollision FlyInto (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
        }


        public override NewCollision FlyInto (CircleCollider c, XY velocity) {
            return -c.FlyInto(this, -velocity);
        }


        public override NewCollision FlyInto (BoxCollider c, XY velocity) {
            bool collided = false;
            var box = Box;
            var cBox = c.Box;
            var p1 = Geometry.Primitive.None;
            var p2 = Geometry.Primitive.None;

            if (box.Right <= cBox.Left && box.Right + velocity.X > cBox.Left) {
                // vx > 0
                float dist = Geom.RayTo1D(box.Right, velocity.X, cBox.Left);
                float dy = velocity.Y * dist;
                if (box.Bottom + dy < cBox.Top && box.Top + dy > cBox.Bottom) {
                    collided = true;
                    velocity *= dist;
                    p1 = Geometry.Primitive.Right(box.Right);
                    p2 = Geometry.Primitive.Left(cBox.Left);
                }
            }
            if (box.Left >= cBox.Right && box.Left + velocity.X < cBox.Right) {
                // vx < 0
                float dist = Geom.RayTo1D(box.Left, velocity.X, cBox.Right);
                float dy = velocity.Y * dist;
                if (box.Bottom + dy < cBox.Top && box.Top + dy > cBox.Bottom) {
                    collided = true;
                    velocity *= dist;
                    p1 = Geometry.Primitive.Left(box.Left);
                    p2 = Geometry.Primitive.Right(cBox.Right);
                }
            }
            if (box.Top <= cBox.Bottom && box.Top + velocity.Y > cBox.Bottom) {
                // vy > 0
                float dist = Geom.RayTo1D(box.Top, velocity.Y, cBox.Bottom);
                float dx = velocity.X * dist;
                if (box.Left + dx < cBox.Right && box.Right + dx > cBox.Left) {
                    collided = true;
                    velocity *= dist;
                    p1 = Geometry.Primitive.Top(box.Top);
                    p2 = Geometry.Primitive.Bottom(cBox.Bottom);
                }
            }
            if (box.Bottom >= cBox.Top && box.Bottom + velocity.Y < cBox.Top) {
                // vy < 0
                float dist = Geom.RayTo1D(box.Bottom, velocity.Y, cBox.Top);
                float dx = velocity.X * dist;
                if (box.Left + dx < cBox.Right && box.Right + dx > cBox.Left) {
                    collided = true;
                    velocity *= dist;
                    p1 = Geometry.Primitive.Bottom(box.Bottom);
                    p2 = Geometry.Primitive.Top(cBox.Top);
                }
            }
            return collided ? new NewCollision(velocity, XY.NaN, this, c, p1, p2) : null;
        }


        public override NewCollision FlyInto (Land land, XY velocity) {
            var collision = land.ApproxCollision(Box, velocity);
            if (collision != null) collision.Collider1 = this;
            return collision;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
            var o = c.Center;
            return XY.SqrDistance(o, o.Clamped(Box)) < c.Radius * c.Radius;
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
