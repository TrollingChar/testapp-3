﻿using System.Collections.Generic;
using System.Linq;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Geometry;
using UnityEngine;
using Utils.Singleton;
using Collider = Battle.Physics.Collisions.Collider;
using Collision = Battle.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace Battle.Objects {

    public abstract class Object {

        private static readonly NullObject _empty = new NullObject();

//        private readonly BF _bf = The<BF>.Get();
        private readonly World _world = The<World>.Get();

        private CollisionHandler _collisionHandler;
        private Controller _controller;
        private Explosive _explosive;

        private XY _position;
        public List<Collider> Colliders;
        public HashSet<Object> Excluded;
        public float Mass;

        public float Movement;
        public LinkedListNode<Object> Node;

        protected GameObject Sprite;
        public int SuperMass;

        public XY Velocity;


        protected Object (float mass = 60f, int superMass = 0) {
            Mass = mass;
            SuperMass = superMass;

            Colliders = new List<Collider>();
            Excluded = new HashSet<Object>();
        }


        public XY Position {
            get { return _position; }
            set {
                _position = value;
                foreach (var c in Colliders) c.UpdatePosition();
            }
        }

        public Controller Controller {
            get { return _controller; }
            set { SwapComponent(ref _controller, value); }
        }

        public Explosive Explosive {
            get { return _explosive; }
            set { SwapComponent(ref _explosive, value); }
        }

        public CollisionHandler CollisionHandler {
            get { return _collisionHandler; }
            set { SwapComponent(ref _collisionHandler, value); }
        }


        public void Update (TurnData td) {
            if (Controller != null) Controller.Update(td);
        }


        public virtual void OnAdd () {
            // Sprite = UnObject.Instantiate(...
            // AddCollider(...
            // Explosive = ...
            // Controller = ...
            // CollisionHandler = ...
        }


        public void Remove () {
            Node.Value = _empty;
            RemoveColliders();
            CollisionHandler = null;
            Explosive = null;
            Controller = null;
            RemoveSprite();
        }


        protected void RemoveColliders () {
            foreach (var c in Colliders) {
                c.FreeTiles();
                //c.Object = null;
            }
        }


        public Collision NextCollision (float movementLeft) {
            var v = Velocity * movementLeft;
            var cObj = CollideWithObjects(v);
            if (cObj != null) v = cObj.Offset;
            var cLand = CollideWithLand(v);
            return cLand ?? cObj;
        }


        private Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var obstacles = new HashSet<Collider>(
                    c.FindObstacles(_world, v)
                        .Where(o => !o.Object.PassableFor(this))
                        .Where(o => !Excluded.Contains(o.Object))
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
                var temp = c.CollideWithLand(_world.Land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping(_world)) {
                Excluded.Add(obstacle.Object);
            }
        }


        public void UpdateSpritePosition () {
            if (Sprite == null) return;
            Sprite.transform.position = new Vector3(Position.X, Position.Y, Sprite.transform.position.z);
        }


        public void Detonate () {
            if (Explosive == null) {
                Remove();
            } else {
                Explosive.Detonate();
            }
        }


        protected void AddCollider (Collider c) {
            c.Object = this;
            Colliders.Add(c);
            c.UpdatePosition();
        }


        protected void RemoveCollider (Collider c) {
            c.FreeTiles();
            Colliders.Remove(c);
        }


        private void RemoveSprite () {
            if (Sprite != null) UnObject.Destroy(Sprite);
        }


        public bool WillCauseCollision (Collision c) {
            return CollisionHandler == null || CollisionHandler.WillCauseCollision(c);
        }


        public void OnCollision (Collision c) {
            if (CollisionHandler != null) CollisionHandler.OnCollision(c);
        }


        private void SwapComponent<T> (ref T component, T newComponent) where T : Component {
            if (component != null) {
                component.OnRemove();
                //component.Object = null;
            }
            if (newComponent != null) {
                newComponent.Object = this;
                newComponent.OnAdd();
            }
            component = newComponent;
        }

    }

}