﻿using System;
using System.Collections.Generic;
using Collisions;
using Geometry;


namespace Battle.Objects {

    
    public class Ray : Object {

        public Object Object;

        public Ray (XY position) : this(position, new CircleCollider(XY.Zero, 0)) {}


        public Ray (XY position, Collider collider) : base(60f, -100) {
            Position = position;
            AddCollider(collider);
            ExcludeObjects();
        }


        public Ray (XY position, IEnumerable<Collider> colliders) : base(60f, -100) {
            Position = position;
            foreach (var c in colliders) AddCollider(c);
            ExcludeObjects();
        }


        public sealed override void OnSpawn () {
            throw new InvalidOperationException("Attempt to add a ray to the world!");
        }


        public Collision Cast (XY direction, bool removeColliders = true) {
            Velocity = direction;
            var collision = NextCollision(1f);
            if (removeColliders) RemoveColliders();
            return collision;
        }

    }

}
