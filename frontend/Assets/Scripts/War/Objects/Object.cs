using System.Collections.Generic;
using System.Linq;
using Geometry;
using UnityEngine;
using Utils.Singleton;
using War.Objects.CollisionHandlers;
using War.Objects.Controllers;
using War.Objects.Explosives;
using Collider = War.Physics.Collisions.Collider;
using Collision = War.Physics.Collisions.Collision;
using UnObject = UnityEngine.Object;


namespace War.Objects {

    public abstract class Object {

        private static readonly NullObject _empty = new NullObject();
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

        protected GameObject Sprite;
        private Controller _controller;
        private Explosive _explosive;
        private CollisionHandler _collisionHandler;
        
        private readonly BF _bf = The<BF>.Get();

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


        protected Object (float mass = 60f, int superMass = 0) {
            Mass = mass;
            SuperMass = superMass;

            Colliders = new List<Collider>();
            Excluded = new HashSet<Object>();
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
            XY v = Velocity * movementLeft;
            var cObj = CollideWithObjects(v);
            if (cObj != null) v = cObj.Offset;
            var cLand = CollideWithLand(v);
            return cLand ?? cObj;
        }


        private Collision CollideWithObjects (XY v) {
            Collision min = null;
            foreach (var c in Colliders) {
                var obstacles = new HashSet<Collider>(
                    c.FindObstacles(_bf.World, v)
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
                var temp = c.CollideWithLand(_bf.World.Land, v);
                if (temp < min) min = temp;
            }
            return min;
        }


        public void ExcludeObjects () {
            foreach (var collider in Colliders)
            foreach (var obstacle in collider.FindOverlapping(_bf.World)) {
                Excluded.Add(obstacle.Object);
            }
        }


        public void UpdateSpritePosition () {
            if (Sprite == null) return;
            Sprite.transform.position = new Vector3(Position.X, Position.Y, Sprite.transform.position.z);
        }


        public void Detonate () {
            if (Explosive == null) Remove();
            else Explosive.Detonate();
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


        private void SwapComponent <T> (ref T component, T newComponent) where T : Component {
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
