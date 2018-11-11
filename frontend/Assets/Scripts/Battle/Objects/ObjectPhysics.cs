using System.Collections.Generic;
using System.Linq;
using Core;
using Geometry;
using UnityEngine;
using Collider = Collisions.Collider;
using Collision = Collisions.Collision;


namespace Battle.Objects {

    public partial class Object {

        private readonly HashSet <Object> Excluded;

        public float Movement;


        public Collision NextCollision (float movementLeft) {
            var v               = Velocity * movementLeft;
            var cObj            = CollideWithObjects (v);
            if (cObj != null) v = cObj.Offset;
            var cLand           = CollideWithLand (v);
            return cLand ?? cObj;
        }


        private Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var obstacles = new HashSet <Collider> (
                    c.FindObstacles (v).
                    Where(o => !o.Object.PassableFor(this) && !Excluded.Contains(o.Object))
                );
                foreach (var o in obstacles) {
                    var temp            = c.FlyInto (o, v);
                    if (temp < min) min = temp;
                }
                obstacles.Clear ();
            }
            return min;
        }


        private Collision CollideWithLand (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var temp            = c.FlyInto (The.World.Land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping ()) {
                Excluded.Add (obstacle.Object);
            }
        }


        public void OnCollision (Collision c) {
            if (CollisionHandler != null) CollisionHandler.OnCollision (c);
        }


        public void PhysicsBegin () {
            Movement = 1;
            Excluded.Clear ();
            ExcludeObjects ();
        }


        public void PhysicsUpdate (bool force) {
            if (Velocity.Length * Movement <= Settings.PhysicsPrecision) return;

            var collision = NextCollision (Movement);
            var world     = The.World;

            if (collision == null) {
                Position += (Movement * Velocity).WithLengthReduced (Settings.PhysicsPrecision);
                Movement =  0;
                goto checkSink;
            }

            collision.ImprovePrecision ();

            if (collision.IsLandCollision) {
                collision.DoMove ();

                var с = collision.Collider1;
                Velocity = Geom.Bounce (
                    Velocity,
                    collision.Normal,
                    Mathf.Sqrt (с.TangentialBounce * world.Land.TangentialBounce),
                    Mathf.Sqrt (с.NormalBounce * world.Land.NormalBounce)
                );
                OnCollision (collision);
            }
            else if (force) {
                collision.DoMove ();
                collision.DoBounceForce ();
            }
            else {
                collision.DoMove ();
                collision.DoBounce ();
            }
            checkSink:
            if (WillSink && Position.Y < world.WaterLevel) Despawn ();
        }


        public void PhysicsEnd () {
            UpdateGameObjectPosition ();
            Velocity *= 1 - Movement;
        }

    }

}