using System;
using Geometry;


namespace Battle.Physics.Collisions {

    public class BoxCollider : Collider {

        private readonly float _leftOffset;
        private readonly float _rightOffset;
        private readonly float _bottomOffset;
        private readonly float _topOffset;


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
            var result = land.CastRectRay(Left, Right, Bottom, Top, v);
            if (result != null) result.Collider1 = this;
            return result;
        }


        public override bool OverlapsWith (Collider c) {
            return c.OverlapsWithBox(this);
        }


        public override bool OverlapsWithCircle (CircleCollider c) {
            return Geom.AreOverlapping(c.Center, c.Radius, Left, Right, Bottom, Top);
        }


        public override bool OverlapsWithBox (BoxCollider c) {
            return Left < c.Right
                && Right > c.Left
                && Bottom < c.Top
                && Top > c.Bottom;
        }


        public override bool OverlapsWithLand (Land land) {
            throw new NotImplementedException();
        }

    }

}
