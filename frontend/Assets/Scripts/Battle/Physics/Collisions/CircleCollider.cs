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
            float mv = Geom.CastRayToCircle(Center, velocity, c.Center, Radius + c.Radius);
            return mv < 1
                ? new Collision(
                    velocity * mv,
                    Center + velocity * mv - c.Center,
                    this,
                    c,
                    CirclePrimitive.New(Center, Radius),
                    CirclePrimitive.New(c.Center, c.Radius)
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
                d = Geom.CastRayToVertical(center, velocity, right + Radius);
                float y = center.Y + velocity.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Right,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        VerticalEdgePrimitive.Right(right)
                    );
                }
            }
            // check left side
            if (velocity.X > 0) {
                d = Geom.CastRayToVertical(center, velocity, left - Radius);
                float y = center.Y + velocity.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Left,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        VerticalEdgePrimitive.Left(left)
                    );
                }
            }
            // check top side
            if (velocity.Y < 0) {
                d = Geom.CastRayToHorizontal(center, velocity, top + Radius);
                float x = center.X + velocity.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Up,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        HorizontalEdgePrimitive.Up(top)
                    );
                }
            }
            // check bottom side
            if (velocity.Y > 0) {
                d = Geom.CastRayToHorizontal(center, velocity, bottom - Radius);
                float x = center.X + velocity.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        XY.Down,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        HorizontalEdgePrimitive.Down(bottom)
                    );
                }
            }
            // check upright corner
            if (velocity.X < 0 || velocity.Y < 0) {
                d = Geom.CastRayToCircle(center, velocity, new XY(right, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(right, top),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(right, top))
                    );
                }
            }
            // check upleft corner
            if (velocity.X > 0 || velocity.Y < 0) {
                d = Geom.CastRayToCircle(center, velocity, new XY(left, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(left, top),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(left, top))
                    );
                }
            }
            // check downleft corner
            if (velocity.X > 0 || velocity.Y > 0) {
                d = Geom.CastRayToCircle(center, velocity, new XY(left, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(left, bottom),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(left, bottom))
                    );
                }
            }
            // check downright corner
            if (velocity.X < 0 || velocity.Y > 0) {
                d = Geom.CastRayToCircle(center, velocity, new XY(right, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        velocity * d,
                        Center + velocity * d - new XY(right, bottom),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(right, bottom))
                    );
                }
            }
            return result;
        }


        public override Collision FlyInto (Land land, XY velocity) {
            var result = land.CastRay(Center, velocity, Radius);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
            float radii = Radius + c.Radius;
            return (Center - c.Center).SqrLength < radii * radii;
        }


        public override bool Overlaps (BoxCollider c) {
            return Geom.AreOverlapping(Center, Radius, c.Left, c.Right, c.Bottom, c.Top);
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }

    }

}
