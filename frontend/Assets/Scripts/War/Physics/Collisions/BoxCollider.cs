
namespace W3 {
    public class BoxCollider : Collider {
        float
            leftOffset,
            rightOffset,
            bottomOffset,
            topOffset;

        public BoxCollider (
            float left,
            float right,
            float bottom,
            float top
        ) {
            leftOffset = left;
            rightOffset = right;
            bottomOffset = bottom;
            topOffset = top;
        }

        public float left { get { return leftOffset + obj.position.x; } }
        public float right { get { return rightOffset + obj.position.x; } }
        public float bottom { get { return bottomOffset + obj.position.y; } }
        public float top { get { return topOffset + obj.position.y; } }

        public override AABBF aabb {
            get { return new AABBF(left, right, bottom, top); }
        }

        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.zero ? null : -c.CollideWithBox(this, -velocity);
        }

        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
        }

        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
        }

        public override Collision CollideWithLand (Land land, XY v) {
        }
    }
}
