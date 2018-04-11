using System.Collections.Generic;
using System.Linq;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Collider = Collisions.Collider;
using Collision = Collisions.Collision;


namespace Battle.Objects {

    public abstract class Object {

        private static readonly NullObject Null = new NullObject();

        protected readonly World World = The.World;

        private CollisionHandler _collisionHandler;
        private Controller _controller;
        private Explosive _explosive;
        private Timer _timer;
        public readonly List<Collider> Colliders;
        
        public readonly HashSet<Object> Excluded;

        public float Mass;
        public int SuperMass;

        public LinkedListNode<Object> Node;

        public GameObject GameObject;

        public float Movement;
        private XY _position;
        public XY Velocity;

        public bool Immobile;


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

        public Timer Timer {
            get { return _timer; }
            set { SwapComponent(ref _timer, value); }
        }


        public void Update (TurnData td) {
            if (Controller != null) Controller.Update(td);
            if (Timer != null) Timer.Update();
        }


        public virtual void OnAdd () {
            // ... = UnityEngine.Object.Instantiate(..., GameObject.transform);
            // AddCollider(...
            // Explosive = ...
            // Controller = ...
            // CollisionHandler = ...
        }


        public void Spawn (Object o, XY position, XY velocity = default(XY)) {
            o.Node = Node.List.AddBefore(Node, o);
            // todo get rid of duplicate code: World.cs
            o.Position = position;
            o.Velocity = velocity;
            o.GameObject = new GameObject(o.GetType().ToString());
            o.OnAdd();
            o.UpdateGameObjectPosition();
        }


        public void Remove () {
            OnRemove();
            
            Node.Value = Null;
            RemoveColliders();
            CollisionHandler = null;
            Explosive = null;
            Controller = null;
            UnityEngine.Object.Destroy(GameObject);
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
                    c.FindObstacles(v)
                        .Where(o => !o.Object.PassableFor(this))
                        .Where(o => !Excluded.Contains(o.Object))
                );
                foreach (var o in obstacles) {
                    var temp = c.FlyInto(o, v);
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
                var temp = c.FlyInto(World.Land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping()) {
                Excluded.Add(obstacle.Object);
            }
        }


        public void UpdateGameObjectPosition () {
            if (GameObject == null) return;
            GameObject.transform.position = new Vector3(Position.X, Position.Y, GameObject.transform.position.z);
        }


        public void Detonate () {
            if (Explosive == null) {
                Remove();
            } else {
                Explosive.Detonate();
            }
        }


        public void AddCollider (Collider c) {
            c.Object = this;
            Colliders.Add(c);
            c.UpdatePosition();
        }


        public void RemoveCollider (Collider c) {
            c.FreeTiles();
            Colliders.Remove(c);
        }


        public bool WillCauseCollision (Collision c) {
            return CollisionHandler == null || CollisionHandler.WillCauseCollision(c);
        }


        public void OnCollision (Collision c) {
            if (CollisionHandler != null) CollisionHandler.OnCollision(c);
        }


        protected void SwapComponent<T> (ref T component, T newComponent) where T : Component {
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


        public virtual void TakeDamage (int damage) {}
        public virtual void TakeHealing (int healing, bool showLabel = true) {}
        public virtual void AddPoison (int dpr, bool additive) {}
        public virtual void CurePoison (int dpr) {}
        public virtual void CureAllPoison () {}


        public virtual void TakeAxeDamage (float factor, int min, int max) { // factor 0.5 - half, 1.0 - all hp
            TakeDamage(min);
        }


        public virtual void TakeGsomDamage () {
            TakeDamage(9999);
        }


        public virtual void ReceiveBlastWave (XY impulse) {
            Velocity += impulse;
        }
        
        public virtual void OnRemove() {}

    }

}
