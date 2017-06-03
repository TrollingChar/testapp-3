
namespace W3 {
    public class BoxCollider : Collider {
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
