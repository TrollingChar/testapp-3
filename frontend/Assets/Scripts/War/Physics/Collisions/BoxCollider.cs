
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

        public override AABBF aabb {
            get { throw new System.NotImplementedException(); }
        }

        public override Collision CollideWith (Collider c, XY velocity) {
            throw new System.NotImplementedException();
        }

        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
            throw new System.NotImplementedException();
        }

        public override Collision CollideWithLand (Land land, XY v) {
            throw new System.NotImplementedException();
        }
    }
}
