using System;
using Geometry;


namespace Battle.Physics.Collisions {

    public class BoxCollider : Collider {

        private readonly float _leftOffset;
        private readonly float _rightOffset;
        private readonly float _bottomOffset;
        private readonly float _topOffset;


        public BoxCollider (float left, float right, float bottom, float top) {
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
            get { return new Box(Left, Right, Top, Bottom); }
        }

        
        public override AABBF AABB {
            get { return new AABBF(Left, Right, Bottom, Top); }
        }


        public override Collision FlyInto (Collider c, XY v) {
            return v == XY.Zero
                ? new Collision(v, XY.NaN, null, null)
                : -c.FlyInto(this, -v);
        }


        public override Collision FlyInto (CircleCollider c, XY v) {
            var on = Geometry.Physics.FlyInto(c.Circle, Box, -v);
            return on.IsEmpty
                ? new Collision(v, XY.NaN, null, null)
                : new Collision(-on.Offset, -on.Normal, this, c);
        }


        public override Collision FlyInto (BoxCollider c, XY v) {
            var on = Geometry.Physics.FlyInto(Box, c.Box, v);
            return on.IsEmpty
                ? new Collision(v, XY.NaN, null, null)
                : new Collision(on.Offset, on.Normal, this, c);
        }


        public override Collision FlyInto (Land land, XY v) {
            var result = land.CastRectRay(Left, Right, Bottom, Top, v);
            if (!result.IsEmpty) result.Collider1 = this;
            return result;
        }


        public override bool Overlaps (Collider c) {
            return c.Overlaps(this);
        }


        public override bool Overlaps (CircleCollider c) {
            return Geometry.Physics.Overlap(c.Circle, Box);
        }


        public override bool Overlaps (BoxCollider c) {
            return Geometry.Physics.Overlap(Box, c.Box);
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
