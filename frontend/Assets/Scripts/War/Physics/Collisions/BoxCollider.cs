using System;
using Geometry;


namespace War.Physics.Collisions {

    public class BoxCollider : Collider {

        private float
            _leftOffset,
            _rightOffset,
            _bottomOffset,
            _topOffset;


        public BoxCollider (
            float left,
            float right,
            float bottom,
            float top
        ) {
            _leftOffset = left;
            _rightOffset = right;
            _bottomOffset = bottom;
            _topOffset = top;
        }


        public float Left {
            get { return _leftOffset + Object.Position.X; }
        }

        public float Right {
            get { return _rightOffset + Object.Position.X; }
        }

        public float Bottom {
            get { return _bottomOffset + Object.Position.Y; }
        }

        public float Top {
            get { return _topOffset + Object.Position.Y; }
        }

        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override Collision CollideWith (Collider c, XY velocity) {
            return velocity == XY.Zero ? null : -c.CollideWithBox(this, -velocity);
        }


        public override Collision CollideWithCircle (CircleCollider c, XY velocity) {
            return null;
        }


        public override Collision CollideWithBox (BoxCollider c, XY velocity) {
            return null;
        }


        public override Collision CollideWithLand (Land land, XY v) {
            Collision result = land.CastRectRay(Left, Right, Bottom, Top, v);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool OverlapsWith (Collider c) {
            throw new NotImplementedException();
        }


        public override bool OverlapsWithCircle (CircleCollider c) {
            throw new NotImplementedException();
        }


        public override bool OverlapsWithBox (BoxCollider c) {
            throw new NotImplementedException();
        }


        public override bool OverlapsWithLand (Land land) {
            throw new NotImplementedException();
        }

    }

}
