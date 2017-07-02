using System;
using Geometry;


namespace War.Physics.Collisions {

    public class CircleCollider : Collider {

        private XY _offset;
        private float _radius;

        public XY Center {
            get {
                //Debug.Log(obj);
                return _offset + Obj.Position;
            }
        }


        public CircleCollider (XY offset, float radius) {
            _offset = offset;
            _radius = radius;
        }


        public override AABBF AABB {
            get {
                XY center = Center;
                return new AABBF(
                    center.X - _radius,
                    center.X + _radius,
                    center.Y - _radius,
                    center.Y + _radius
                );
            }
        }


        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.CollideWithCircle(this, -velocity);
        }


        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
            float mv = Geom.CastRayToCircle(Center, velocity, c.Center, _radius + c._radius);
            return mv < 1
                ? new Collision(
                    velocity * mv,
                    Center + velocity * mv - c.Center,
                    this,
                    c,
                    CirclePrimitive.New(Center, _radius),
                    CirclePrimitive.New(c.Center, c._radius)
                ) : null;
        }


        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
            return null;
        }


        public override Collision CollideWithLand (Land land, XY v) {
            Collision result = land.CastRay(Center, v, _radius);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool OverlapsWith (Collider c) {
            return c.OverlapsWithCircle(this);
        }


        public override bool OverlapsWithCircle (CircleCollider c) {
            float radii = _radius + c._radius;
            return (Center - c.Center).SqrLength < radii * radii;
        }


        public override bool OverlapsWithBox (BoxCollider c) {
            throw new NotImplementedException();
        }


        public override bool OverlapsWithLand (Land land) {
            throw new NotImplementedException();
        }

    }

}
