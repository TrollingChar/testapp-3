using System;
using Collisions;
using Geometry;


namespace Battle.Physics.Collisions {

    public class BoxCollider : Collider {

        private readonly float _leftOffset;
        private readonly float _rightOffset;
        private readonly float _bottomOffset;
        private readonly float _topOffset;


        public BoxCollider(float left, float right, float bottom, float top) {
            _topOffset = top;
            _leftOffset = left;
            _rightOffset = right;
            _bottomOffset = bottom;
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

        
        public Box Box {
            get {return new Box(Left, Right, Bottom, Top);}
        }

        
        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override NewCollision FlyInto (Collider c, XY velocity) {
            throw new NotImplementedException();
        }


        public override NewCollision FlyInto (CircleCollider c, XY velocity) {
            throw new NotImplementedException();
        }


        public override NewCollision FlyInto (BoxCollider c, XY velocity) {
            throw new NotImplementedException();
        }


        public override NewCollision FlyInto (Land land, XY velocity) {
            var collision = land.ApproxCollision(Box, velocity);
            if (collision != null) collision.Collider1 = this;
            return collision;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c)
        {
            XY o = c.Center;
            return XY.SqrDistance(o, o.Clamped(Box)) < c.Radius * c.Radius;
        }


        public override bool Overlaps (BoxCollider c) {
            return Left < c.Right
                && Right > c.Left
                && Bottom < c.Top
                && Top > c.Bottom;
        }


        public override bool Overlaps (Land land) {
            throw new NotImplementedException();
        }


        public override string ToString () {
            return "Box: left:" + Left.ToString("R") + ", right:" + Right.ToString("R")
                + ", bottom:" + Bottom.ToString("R") + ", top:" + Top.ToString("R");
        }

    }

}
