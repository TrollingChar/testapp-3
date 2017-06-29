using UnityEngine;

namespace W3 {
    public class CircleCollider : Collider {
        XY offset;
        float radius;
        public XY center {
            get {
                //Debug.Log(obj);
                return offset + obj.position;
            }
        }

        public CircleCollider (XY offset, float radius)
            : base() {
            this.offset = offset;
            this.radius = radius;
        }

        public override AABBF aabb {
            get {
                XY center = this.center;
                return new AABBF(
                    center.x - radius,
                    center.x + radius,
                    center.y - radius,
                    center.y + radius
                );
            }
        }

        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.zero ? null : -c.CollideWithCircle(this, -velocity);
        }

        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
            float mv = Geom.CastRayToCircle(center, velocity, c.center, radius + c.radius);
            return mv < 1 ? new Collision(
                velocity * mv,
                center + velocity * mv - c.center,
                this,
                c,
                CirclePrimitive.New(center, radius),
                CirclePrimitive.New(c.center, c.radius)
            ) : null;
        }

        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
            return null;
        }

        public override Collision CollideWithLand (Land land, XY v) {
            Collision result = land.CastRay(center, v, radius);
            if (result != null) result.collider1 = this;
            return result;
        }

        public override bool OverlapsWith (Collider c) {
            return c.OverlapsWithCircle(this);
        }

        public override bool OverlapsWithCircle (CircleCollider c) {
            float radii = radius + c.radius;
            return (center - c.center).sqrLength < radii * radii;
        }

        public override bool OverlapsWithBox (BoxCollider c) {
            throw new System.NotImplementedException();
        }

        public override bool OverlapsWithLand (Land land) {
            throw new System.NotImplementedException();
        }
    }
}