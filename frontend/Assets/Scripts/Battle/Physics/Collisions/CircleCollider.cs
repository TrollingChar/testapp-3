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


        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.CollideWithCircle(this, -velocity);
        }


        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
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


        public override Collision CollideWithBox (BoxCollider c, XY v) {
            float
                left = c.Left,
                right = c.Right,
                bottom = c.Bottom,
                top = c.Top;

            float minDist = 1;
            Collision result = null;

            XY center = Center;
            float d;
            // check right side
            if (v.X < 0) {
                d = Geom.CastRayToVertical(center, v, right + Radius);
                float y = center.Y + v.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Right,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        VerticalEdgePrimitive.Right(right)
                    );
                }
            }
            // check left side
            if (v.X > 0) {
                d = Geom.CastRayToVertical(center, v, left - Radius);
                float y = center.Y + v.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Left,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        VerticalEdgePrimitive.Left(left)
                    );
                }
            }
            // check top side
            if (v.Y < 0) {
                d = Geom.CastRayToHorizontal(center, v, top + Radius);
                float x = center.X + v.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Up,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        HorizontalEdgePrimitive.Up(top)
                    );
                }
            }
            // check bottom side
            if (v.Y > 0) {
                d = Geom.CastRayToHorizontal(center, v, bottom - Radius);
                float x = center.X + v.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Down,
                        this,
                        c,
                        CirclePrimitive.New(center, Radius),
                        HorizontalEdgePrimitive.Down(bottom)
                    );
                }
            }
            // check upright corner
            if (v.X < 0 || v.Y < 0) {
                d = Geom.CastRayToCircle(center, v, new XY(right, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(right, top),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(right, top))
                    );
                }
            }
            // check upleft corner
            if (v.X > 0 || v.Y < 0) {
                d = Geom.CastRayToCircle(center, v, new XY(left, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(left, top),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(left, top))
                    );
                }
            }
            // check downleft corner
            if (v.X > 0 || v.Y > 0) {
                d = Geom.CastRayToCircle(center, v, new XY(left, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(left, bottom),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(left, bottom))
                    );
                }
            }
            // check downright corner
            if (v.X < 0 || v.Y > 0) {
                d = Geom.CastRayToCircle(center, v, new XY(right, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(right, bottom),
                        this,
                        c,
                        CirclePrimitive.New(Center, Radius),
                        CirclePrimitive.New(new XY(right, bottom))
                    );
                }
            }
            return result;
        }


        public override Collision CollideWithLand (Land land, XY v) {
            var result = land.CastRay(Center, v, Radius);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool OverlapsWith (Collider c) {
            return c.OverlapsWithCircle(this);
        }


        public override bool OverlapsWithCircle (CircleCollider c) {
            float radii = Radius + c.Radius;
            return (Center - c.Center).SqrLength < radii * radii;
        }


        public override bool OverlapsWithBox (BoxCollider c) {
            return Geom.AreOverlapping(Center, Radius, c.Left, c.Right, c.Bottom, c.Top);
        }


        public override bool OverlapsWithLand (Land land) {
            throw new NotImplementedException();
        }

    }

}
