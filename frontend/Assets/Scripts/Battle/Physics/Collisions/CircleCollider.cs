using System;
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


        public override Collision FlyInto (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
        }


        public override Collision FlyInto (CircleCollider c, XY velocity) {
            float mv = OldGeom.CastRayToCircle(Center, velocity, c.Center, Radius + c.Radius);
            return mv < 1
                ? new Collision(
                    velocity * mv,
                    Center + velocity * mv - c.Center,
                    this,
                    c
                ) : null;
        }


        public override Collision FlyInto (BoxCollider c, XY velocity) {
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
            return result;
        }


        public override Collision FlyInto (Land land, XY velocity) {
//            var result = land.CastRay(Center, velocity, Radius);
//            if (result != null) result.Collider1 = this;
//            return result;
//            throw new NotImplementedException();
            var collision = land.ApproxCollision(Circle, velocity);
            if (collision != null) collision.C1 = this;
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
            return OldGeom.AreOverlapping(Center, Radius, c.Left, c.Right, c.Bottom, c.Top);
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Circle: center:" + Center.ToString("R") + ", radius:" + Radius.ToString("R");
        }

    }

}
