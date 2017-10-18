using System;
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


        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
            return null;
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
