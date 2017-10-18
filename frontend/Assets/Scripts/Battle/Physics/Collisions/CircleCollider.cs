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
            float
                left = c.Left,
                right = c.Right,
                bottom = c.Bottom,
                top = c.Top;
            
            float minDist = 1;
            Collision result = null;

            // todo: еще проверить будет ли лежать ли точка окружности на отрезке
            if (velocity.X < 0) {
                float mv = Geom.CastRayToVertical(Center, velocity, right + Radius);
            } // check right  side
            if (velocity.X > 0) {
                float mv = Geom.CastRayToVertical(Center, velocity, left - Radius);
            } // check left   side
            if (velocity.Y < 0) {
                float mv = Geom.CastRayToHorizontal(Center, velocity, top + Radius);
            } // check top    side
            if (velocity.Y > 0) {
                float mv = Geom.CastRayToHorizontal(Center, velocity, bottom - Radius);
            } // check bottom side
            if (velocity.X < 0 || velocity.Y < 0) ; // check upright   corner
            if (velocity.X > 0 || velocity.Y < 0) ; // check upleft    corner
            if (velocity.X > 0 || velocity.Y > 0) ; // check downleft  corner
            if (velocity.X < 0 || velocity.Y > 0) ; // check downright corner
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
