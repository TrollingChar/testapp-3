using System;
using Collisions;
using Geometry;


namespace Battle.Physics.Collisions {

    public class CircleCollider : Collider {

        private readonly XY _offset;


        public CircleCollider (XY offset, float radius) {
            _offset = offset;
            Radius = radius;
        }


        public XY Center {
            get { return _offset + Object.Position; }
        }


        public float Radius { get; private set; }

        public Circle Circle {
            get { return new Circle(Center, Radius); }
        }


        public override AABBF AABB {
            get {
                var center = Center;
                return new AABBF(
                    center.X - Radius,
                    center.X + Radius,
                    center.Y - Radius,
                    center.Y + Radius
                );
            }
        }


        public override Collision FlyInto (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
        }


        private bool CollideWithPoint (Circle c, ref XY v, XY point, ref Primitive primitive) {
            float dist = Geom.RayToCircle(c.Center, v, point, c.Radius);
            if (float.IsNaN(dist) || dist < 0 || dist * dist >= v.SqrLength) return false;
            v.Length = dist;
            primitive = Primitive.Circle(point);
            return true;
        }


        public override Collision FlyInto (CircleCollider c, XY velocity) {
            float dist = Geom.RayToCircle(Center, velocity, c.Center, Radius + c.Radius);
            if (float.IsNaN(dist) || dist < 0 || dist * dist >= velocity.SqrLength) return null;
            return new Collision(
                velocity.WithLength(dist),
                XY.NaN,
                this,
                c,
                Primitive.Circle(Circle),
                Primitive.Circle(c.Circle)
            );
        }


        public override Collision FlyInto (BoxCollider c, XY velocity) {
            var circle = Circle;
            var box = c.Box;

            bool collided = false;
            var primitive = Primitive.None;

            XY point;

            point = new XY(circle.Center.X + circle.Radius, circle.Center.Y);
            if (point.X <= box.Left && point.X + velocity.X > box.Left) {
                float dist = Geom.RayTo1D(point.X, velocity.X, box.Left);
                float y = point.Y + dist * velocity.Y;
                if (box.Bottom <= y && y <= box.Top) {
                    collided = true;
                    velocity *= dist;
                    primitive = Primitive.Left(box.Left);
                }
            }

            point = new XY(circle.Center.X - circle.Radius, circle.Center.Y);
            if (point.X >= box.Right && point.X + velocity.X < box.Right) {
                float dist = Geom.RayTo1D(point.X, velocity.X, box.Right);
                float y = point.Y + dist * velocity.Y;
                if (box.Bottom <= y && y <= box.Top) {
                    collided = true;
                    velocity *= dist;
                    primitive = Primitive.Right(box.Right);
                }
            }

            point = new XY(circle.Center.X, circle.Center.Y - circle.Radius);
            if (point.Y >= box.Top && point.Y + velocity.Y < box.Top) {
                float dist = Geom.RayTo1D(point.Y, velocity.Y, box.Top);
                float x = point.X + dist * velocity.X;
                if (box.Left <= x && x <= box.Right) {
                    collided = true;
                    velocity *= dist;
                    primitive = Primitive.Top(box.Top);
                }
            }

            point = new XY(circle.Center.X, circle.Center.Y + circle.Radius);
            if (point.Y <= box.Bottom && point.Y + velocity.Y > box.Bottom) {
                float dist = Geom.RayTo1D(point.Y, velocity.Y, box.Bottom);
                float x = point.X + dist * velocity.X;
                if (box.Left <= x && x <= box.Right) {
                    collided = true;
                    velocity *= dist;
                    primitive = Primitive.Bottom(box.Bottom);
                }
            }

            if (velocity.X > 0 || velocity.Y > 0) {
                collided |= CollideWithPoint(circle, ref velocity, new XY(box.Left, box.Bottom), ref primitive);
            }
            if (velocity.X > 0 || velocity.Y < 0) {
                collided |= CollideWithPoint(circle, ref velocity, new XY(box.Left, box.Top), ref primitive);
            }
            if (velocity.X < 0 || velocity.Y < 0) {
                collided |= CollideWithPoint(circle, ref velocity, new XY(box.Right, box.Top), ref primitive);
            }
            if (velocity.X < 0 || velocity.Y > 0) {
                collided |= CollideWithPoint(circle, ref velocity, new XY(box.Right, box.Bottom), ref primitive);
            }

            return collided
                ? new Collision(velocity, XY.NaN, this, c, Primitive.Circle(circle), primitive)
                : null;
        }


        public override Collision FlyInto (Land land, XY velocity) {
            var collision = land.ApproxCollision(Circle, velocity);
            if (collision != null) collision.Collider1 = this;
            return collision;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
            float radii = Radius + c.Radius;
            return (Center - c.Center).SqrLength < radii * radii;
        }


        public override bool Overlaps (BoxCollider c) {
            var o = Center;
            return XY.SqrDistance(o, o.Clamped(c.Box)) < Radius * Radius;
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Circle: center:" + Center.ToString("R") + ", radius:" + Radius.ToString("R");
        }

    }

}
