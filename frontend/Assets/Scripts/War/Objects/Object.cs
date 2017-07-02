using System.Collections.Generic;
using System.Linq;
using Geometry;
using UnityEngine;
using War.Controllers;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public abstract class Object {

        private static NullObject _empty = new NullObject();
        public LinkedListNode<Object> Node;

        public float Movement;
        public HashSet<Object> Excluded;
        public List<Collider> Colliders;
        public int SuperMass;
        public float Mass;

        private XY _position;
        public XY Position {
            get { return _position; }
            set {
                _position = value;
                foreach (var c in Colliders) c.UpdatePosition();
            }
        }

        public XY Velocity;

        private Controller _controller;
        public Controller Controller {
            get { return _controller; }
            set {
                if (_controller != null) {
                    _controller.OnRemove();
                    _controller.Obj = null;
                }
                if (value != null) {
                    value.Obj = this;
                    value.OnAdd();
                }
                _controller = value;
            }
        }

        protected GameObject Sprite;


        protected Object (float mass = 60f, int superMass = 0) {
            Mass = mass;
            SuperMass = superMass;

            Colliders = new List<Collider>();
            Excluded = new HashSet<Object>();
        }


        public void Update () {
            if (Controller != null) Controller.Update();
        }


        public virtual void OnAdd () {
            InitSprite();
            InitColliders();
            InitController();
        }


        public void Remove () {
            Node.Value = _empty;
            foreach (var c in Colliders) {
                c.FreeTiles();
                c.Obj = null;
            }
            RemoveSprite();
        }


        public Collision NextCollision () {
            XY v = Velocity * Movement;
            var cObj = CollideWithObjects(v);
            if (cObj != null) v = cObj.Offset;
            var cLand = CollideWithLand(v);
            return cLand ?? cObj;
        }


        private Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var obstacles = new HashSet<Collider>(
                    c.FindObstacles(Core.BF.World, v)
                        .Where(o => !o.Obj.PassableFor(this))
                        .Where(o => !Excluded.Contains(o.Obj))
                );
                foreach (var o in obstacles) {
                    var temp = c.CollideWith(o, v);
                    if (temp < min) min = temp;
                }
                obstacles.Clear();
            }
            return min;
        }


        protected virtual bool PassableFor (Object o) {
            return true;
        }


        private Collision CollideWithLand (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var temp = c.CollideWithLand(Core.BF.World.Land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping(Core.BF.World)) {
                Excluded.Add(obstacle.Obj);
            }
        }


        public void UpdateSpritePosition () {
            if (Sprite == null) return;
            Sprite.transform.position = new Vector3(Position.X, Position.Y, Sprite.transform.position.z);
        }


        public virtual void Detonate () {
            Remove();
        }


        protected virtual void InitColliders () {
            // AddCollider(...);
            // AddCollider(...);
        }


        protected void AddCollider (Collider c) {
            c.Obj = this;
            Colliders.Add(c);
            c.UpdatePosition();
        }


        protected void RemoveCollider (Collider c) {
            c.FreeTiles();
            Colliders.Remove(c);
        }


        protected virtual void InitController () {
            // Controller = ...
        }


        protected virtual void InitSprite () {
            // Sprite = GameObject.Instantiate(...);
        }


        private void RemoveSprite () {
            if (Sprite != null) UnObject.Destroy(Sprite);
        }


        public virtual void OnCollision (Collision c) {
            // empty by default
        }

    }

}
