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
            return null;
        }

        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
            return null;
        }

        public override Collision CollideWithLand (Land land, XY v) {
            Collision result = land.CastRay(center, v, radius);
            if (result != null) result.collider1 = this;
            return result;
        }
    }
}