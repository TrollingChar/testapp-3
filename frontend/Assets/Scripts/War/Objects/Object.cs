﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public abstract class Object {
        static Object empty = new NullObject();
        public LinkedListNode<Object> node;

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
                if (_controller != null) _controller.OnRemove();
                if (value != null) {
                    value.obj = this;
                    value.OnAdd();
                }
                _controller = value;
            }
        }

        protected GameObject sprite;

        public float movement;
        public HashSet<Object> excluded;
        public List<Collider> colliders;
        public float mass;

        public Object (float mass = 60) {
            this.mass = mass;
            colliders = new List<Collider>();
        }

        public void Update () {
            controller.Update();
        }

        virtual public void OnAdd () {
            InitColliders();
            InitController();
            InitSprite();
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
            return cLand == null ? cObj : cLand;
        }

        Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in colliders) {
                HashSet<Collider> obstacles = new HashSet<Collider>(
                    c.FindObstacles(Core.I.bf.world, v)
                    .Where(o => !o.obj.PassableFor(this))
                    .Where(o => !excluded.Contains(o.obj)));
                foreach (var o in obstacles) {
                    var temp = c.CollideWith(o, v);
                    if (temp < min) min = temp;
                }
                obstacles.Clear();
            }
            return min;
        }

        virtual protected bool PassableFor (Object o) {
            return true;
        }

        Collision CollideWithLand (XY v) {
            Collision min = null;
            foreach (var c in colliders) {
                var temp = c.CollideWithLand(Core.I.bf.world.land, v);
                if (temp < min) min = temp;
            }
            return min;
        }

        public void ExcludeObjects () {
            throw new NotImplementedException();
        }

        public void UpdateSpritePosition () {
            throw new NotImplementedException();
        }

        virtual public void Detonate () {
            Remove();
        }

        virtual protected void InitColliders () {
            // AddCollider(...);
            // AddCollider(...);
        }

        void AddCollider (Collider c) {
            c.obj = this;
            colliders.Add(c);
            c.UpdatePosition();
        }

        void RemoveCollider (Collider c) {
            c.FreeTiles();
            colliders.Remove(c);
        }

        virtual protected void InitController () {
            // controller = ...
        }

        virtual protected void InitSprite () {
        }

        virtual protected void RemoveSprite () {
        }
    }
}