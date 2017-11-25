using System;
using Geometry;
using Collision = Geometry.Collision;


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


        public override Collision FlyInto (Collider c, XY v) {
//            return velocity == XY.Zero ? null : -c.FlyInto(this, -velocity);
            return v == XY.Zero
                ? new Collision(v, XY.NaN, null, null)
                : -c.FlyInto(this, -v);
        }


        public override Collision FlyInto (CircleCollider c, XY v) {
//            float mv = Geom_.CastRayToCircle(Center, v, c.Center, Radius + c.Radius);
//            return mv < 1
//                ? new Collision(
//                    v * mv,
//                    Center + v * mv - c.Center,
//                    this,
//                    c
//                ) : null;
        }


        public override Collision FlyInto (BoxCollider c, XY v) {
            /*
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
            if (v.X < 0) {
                d = Geom_.CastRayToVertical(center, v, right + Radius);
                float y = center.Y + v.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Right,
                        this,
                        c
                    );
                }
            }
            // check left side
            if (v.X > 0) {
                d = Geom_.CastRayToVertical(center, v, left - Radius);
                float y = center.Y + v.Y * d;
                if (d < minDist && bottom <= y && y <= top) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Left,
                        this,
                        c
                    );
                }
            }
            // check top side
            if (v.Y < 0) {
                d = Geom_.CastRayToHorizontal(center, v, top + Radius);
                float x = center.X + v.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Up,
                        this,
                        c
                    );
                }
            }
            // check bottom side
            if (v.Y > 0) {
                d = Geom_.CastRayToHorizontal(center, v, bottom - Radius);
                float x = center.X + v.X * d;
                if (d < minDist && left <= x && x <= right) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        XY.Down,
                        this,
                        c
                    );
                }
            }
            // check upright corner
            if (v.X < 0 || v.Y < 0) {
                d = Geom_.CastRayToCircle(center, v, new XY(right, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(right, top),
                        this,
                        c
                    );
                }
            }
            // check upleft corner
            if (v.X > 0 || v.Y < 0) {
                d = Geom_.CastRayToCircle(center, v, new XY(left, top), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(left, top),
                        this,
                        c
                    );
                }
            }
            // check downleft corner
            if (v.X > 0 || v.Y > 0) {
                d = Geom_.CastRayToCircle(center, v, new XY(left, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(left, bottom),
                        this,
                        c
                    );
                }
            }
            // check downright corner
            if (v.X < 0 || v.Y > 0) {
                d = Geom_.CastRayToCircle(center, v, new XY(right, bottom), Radius);
                if (d < minDist) {
                    minDist = d;
                    result = new Collision(
                        v * d,
                        Center + v * d - new XY(right, bottom),
                        this,
                        c
                    );
                }
            }
            return result;
            */
        }


        public override Collision FlyInto (Land land, XY v) {
            var result = land.CastRay(Center, v, Radius);
            if (!result.IsEmpty) result.Collider1 = this;
            return result;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
//            float radii = Radius + c.Radius;
//            return (Center - c.Center).SqrLength < radii * radii;
        }


        public override bool Overlaps (BoxCollider c) {
//            return Geom_.AreOverlapping(Center, Radius, c.Left, c.Right, c.Bottom, c.Top);
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Circle: center:" + Center.ToString("R") + ", radius:" + Radius.ToString("R");
        }

    }

}
