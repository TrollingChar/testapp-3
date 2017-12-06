using System;
using Collisions;
using Geometry;
using UnityEditor;
using UnityEngine;


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
            get {return new Circle(Center, Radius);}
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


        public override NewCollision FlyInto (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
        }


        public override NewCollision FlyInto (CircleCollider c, XY velocity) {
            float dist = Geom.RayToCircle(Center, velocity, c.Center, Radius + c.Radius);
            if (float.IsNaN(dist) || dist < 0 || dist * dist >= velocity.SqrLength) return null;
            return new NewCollision(
                velocity.WithLength(dist),
                XY.NaN,
                this,
                c,
                Geometry.Primitive.Circle(Circle),
                Geometry.Primitive.Circle(c.Circle)
            );
        }


        public override NewCollision FlyInto (BoxCollider c, XY velocity) {
            float
                left = c.Left,
                right = c.Right,
                bottom = c.Bottom,
                top = c.Top;

            float minDist = 1;
            Collision result = null;

            var center = Center;
            float d;
            // check right side
            if (velocity.X < 0) {
                d = OldGeom.CastRayToVertical(center, velocity, right + Radius);
                float y = center.Y + velocity.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Right,
                        this,
                        c
                    );
                }
            }
            // check left side
            if (velocity.X > 0) {
                d = OldGeom.CastRayToVertical(center, velocity, left - Radius);
                float y = center.Y + velocity.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Left,
                        this,
                        c
                    );
                }
            }
            // check top side
            if (velocity.Y < 0) {
                d = OldGeom.CastRayToHorizontal(center, velocity, top + Radius);
                float x = center.X + velocity.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Up,
                        this,
                        c
                    );
                }
            }
            // check bottom side
            if (velocity.Y > 0) {
                d = OldGeom.CastRayToHorizontal(center, velocity, bottom - Radius);
                float x = center.X + velocity.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Down,
                        this,
                        c
                    );
                }
            }
            // check upright corner
            if (velocity.X < 0 || velocity.Y < 0) {
                d = OldGeom.CastRayToCircle(center, velocity, new XY(right, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(right, top),
                        this,
                        c
                    );
                }
            }
            // check upleft corner
            if (velocity.X > 0 || velocity.Y < 0) {
                d = OldGeom.CastRayToCircle(center, velocity, new XY(left, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(left, top),
                        this,
                        c
                    );
                }
            }
            // check downleft corner
            if (velocity.X > 0 || velocity.Y > 0) {
                d = OldGeom.CastRayToCircle(center, velocity, new XY(left, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(left, bottom),
                        this,
                        c
                    );
                }
            }
            // check downright corner
            if (velocity.X < 0 || velocity.Y > 0) {
                d = OldGeom.CastRayToCircle(center, velocity, new XY(right, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(right, bottom),
                        this,
                        c
                    );
                }
            }
            return null;
        }


        public override NewCollision FlyInto (Land land, XY velocity) {
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
            XY o = Center;
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
