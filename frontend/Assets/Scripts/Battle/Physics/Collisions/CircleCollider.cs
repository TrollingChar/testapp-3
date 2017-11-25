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
            return v == XY.Zero
                ? new Collision(v, XY.NaN, null, null)
                : -c.FlyInto(this, -v);
        }


        public override Collision FlyInto (CircleCollider c, XY v) {
            var on = Geometry.Physics.FlyInto(Circle, c.Circle, v);
            return on.IsEmpty
                ? new Collision(v, XY.NaN, null, null)
                : new Collision(on.Offset, on.Normal, this, c);
        }


        public override Collision FlyInto (BoxCollider c, XY v) {
            var on = Geometry.Physics.FlyInto(Circle, c.Box, v);
            return on.IsEmpty
                ? new Collision(v, XY.NaN, null, null)
                : new Collision(on.Offset, on.Normal, this, c);
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
            return Geometry.Physics.Overlap(Circle, c.Circle);
        }


        public override bool Overlaps (BoxCollider c) {
            return Geometry.Physics.Overlap(Circle, c.Box);
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Circle: center:" + Center.ToString("R") + ", radius:" + Radius.ToString("R");
        }

    }

}
