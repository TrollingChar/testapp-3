using System.Collections.Generic;
using System.Linq;
using Collisions;
using Geometry;


namespace Battle.Objects {

    public class MeleeTester : Object {

        public override void OnAdd () {
            float headRadius = Worm.HeadRadius * 0.9f;
            float bodyHeight = Worm.BodyHeight;

            AddCollider(new CircleCollider(new XY(0f, bodyHeight *  0.5f), headRadius));
            AddCollider(new CircleCollider(new XY(0f, bodyHeight * -0.5f), headRadius));
            AddCollider(new BoxCollider(-headRadius, headRadius, bodyHeight * -0.5f, bodyHeight * 0.5f));
        }


        public IEnumerable<Object> Test () {
            var result = new HashSet<Object>();
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping()) {
                result.Add(obstacle.Object);
            }
            foreach (var c in Colliders) {
                var obstacles = new HashSet<Collider>(c.FindObstacles(Velocity));
                foreach (var o in obstacles) {
                    var temp = c.FlyInto(o, Velocity);
                    if (temp != null) result.Add(o.Object);
                }
                obstacles.Clear();
            }
            result.Remove(this);
            return result;
        }

    }

}
