using Geometry;
using War.Physics.Collisions;


namespace War.Objects {

    internal class Tester : Object {

        public override void OnAdd () {
            AddCollider(new CircleCollider(new XY(0f, Worm.BodyHeight * 0.5f), Worm.HeadRadius));
        }

    }

}
