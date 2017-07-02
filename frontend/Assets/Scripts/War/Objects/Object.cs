using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace W3 {

    public abstract class Object {

        private static NullObject empty = new NullObject();
        public LinkedListNode<Object> node;

        public float movement;
        public HashSet<Object> excluded;
        public List<Collider> colliders;
        public int superMass;
        public float mass;

        public XY _position, velocity;

        public XY position {
            get { return _position; }
            set {
                _position = value;
                foreach (var c in colliders) c.UpdatePosition();
            }
        }

        public Controller _controller;

        public Controller controller {
            get { return _controller; }
            set {
                if (_controller != null) {
                    _controller.OnRemove();
                    _controller.obj = null;
                }
                if (value != null) {
                    value.obj = this;
                    value.OnAdd();
                }
                _controller = value;
            }
        }

        protected GameObject sprite;


        protected Object (float mass = 60f, int superMass = 0) {
            this.mass = mass;
            this.superMass = superMass;

            colliders = new List<Collider>();
            excluded = new HashSet<Object>();
        }


        public void Update () {
            if (controller != null) controller.Update();
        }


        public virtual void OnAdd () {
            InitSprite();
            InitColliders();
            InitController();
        }


        public void Remove () {
            node.Value = empty;
            foreach (var c in colliders) {
                c.FreeTiles();
                c.obj = null;
            }
            RemoveSprite();
        }


        public Collision NextCollision () {
            XY v = velocity * movement;
            var cObj = CollideWithObjects(v);
            if (cObj != null) v = cObj.offset;
            var cLand = CollideWithLand(v);
            //if (cLand != null) Debug.Log("hit!");
            return cLand ?? cObj;
        }


        private Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in colliders) {
                var obstacles = new HashSet<Collider>(
                    c.FindObstacles(Core.bf.world, v)
                        .Where(o => !o.obj.PassableFor(this))
                        .Where(o => !excluded.Contains(o.obj))
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
            foreach (var c in colliders) {
                var temp = c.CollideWithLand(Core.bf.world.land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in colliders)
            foreach (var obstacle in collider.FindOverlapping(Core.bf.world)) {
                excluded.Add(obstacle.obj);
            }
        }


        public void UpdateSpritePosition () {
            if (sprite == null) return;
            sprite.transform.position = new Vector3(position.x, position.y, sprite.transform.position.z);
        }


        public virtual void Detonate () {
            Remove();
        }


        protected virtual void InitColliders () {
            // AddCollider(...);
            // AddCollider(...);
        }


        protected void AddCollider (Collider c) {
            c.obj = this;
            colliders.Add(c);
            c.UpdatePosition();
        }


        protected void RemoveCollider (Collider c) {
            c.FreeTiles();
            colliders.Remove(c);
        }


        protected virtual void InitController () {
            // controller = ...
        }


        protected virtual void InitSprite () {
            // sprite = GameObject.Instantiate(...);
        }


        private void RemoveSprite () {
            if (sprite != null) GameObject.Destroy(sprite);
        }


        public virtual void OnCollision (Collision c) {
            // empty by default
        }

    }

}
