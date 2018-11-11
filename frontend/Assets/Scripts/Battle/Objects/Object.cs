using System.Collections.Generic;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Collider = Collisions.Collider;


namespace Battle.Objects {

    public partial class Object {

        public  GameObject       GameObject { get; set; }
        public  List <Collider>  Colliders  { get; private set; }
        private CollisionHandler _collisionHandler;
        private Controller       _controller;
        private Explosive        _explosive;
        private Timer            _timer;

        public float Mass;
        public int   SuperMass;

        private XY _position;
        public  XY Velocity;

        public bool Despawned;


        protected Object (float mass = 60f, int superMass = 0) {
            Mass       = mass;
            SuperMass  = superMass;
            Colliders = new List <Collider> ();
            Movement   = 1;
            Excluded   = new HashSet <Object> ();
        }


        public XY Position {
            get { return _position; }
            set {
                _position = value;
                foreach (var c in Colliders) c.UpdatePosition ();
            }
        }


        public Controller Controller {
            get { return _controller; }
            set { SwapComponent (ref _controller, value); }
        }


        public Explosive Explosive {
            get { return _explosive; }
            set { SwapComponent (ref _explosive, value); }
        }


        public CollisionHandler CollisionHandler {
            get { return _collisionHandler; }
            set { SwapComponent (ref _collisionHandler, value); }
        }


        public Timer Timer {
            get { return _timer; }
            set { SwapComponent (ref _timer, value); }
        }


        public void Update (TurnData td) {
            if (Controller != null) Controller.Update (td);
            if (Timer != null) Timer.Update ();
        }


        public virtual void OnSpawn () {
            // ... = UnityEngine.Object.Instantiate(..., GameObject.transform);
            // AddCollider(...
            // Explosive = ...
            // Controller = ...
            // CollisionHandler = ...
        }


        public void Despawn () {
            if (Despawned) return;
            Despawned = true;
            OnDespawn ();
            RemoveColliders ();
//            CollisionHandler = null;
//            Explosive        = null;
//            Controller       = null;
            UnityEngine.Object.Destroy (GameObject);
        }


        protected virtual void OnDespawn () {}


        protected void RemoveColliders () {
            foreach (var c in Colliders) c.FreeTiles ();
        }


        public void UpdateGameObjectPosition () {
            if (GameObject == null) return;
            GameObject.transform.position = new Vector3 (Position.X, Position.Y, GameObject.transform.position.z);
        }


        public void Detonate () {
            if (Explosive == null) Despawn ();
            else                   Explosive.Detonate ();
        }


        public void AddCollider (Collider c) {
            c.Object = this;
            Colliders.Add (c);
            c.UpdatePosition ();
            OnColliderAdded (c);
        }


        protected virtual void OnColliderAdded (Collider c) {}


        public void RemoveCollider (Collider c) {
            c.FreeTiles ();
            Colliders.Remove (c);
        }


        protected virtual bool PassableFor (Object o) { return true; }
        public virtual    bool PushableFor (Object o) { return SuperMass <= o.SuperMass; }


        public virtual bool WillSink { get { return true; } }


        protected void SwapComponent <T> (ref T component, T newComponent) where T : Component {
            if (component != null) {
                component.OnRemove ();
                //component.Object = null;
            }
            if (newComponent != null) {
                newComponent.Object = this;
                newComponent.OnAdd ();
            }
            component = newComponent;
        }


        public virtual void TakeDamage    (int damage)                         {}
        public virtual void TakeHealing   (int healing, bool showLabel = true) {}
        public virtual void AddPoison     (int dpr, bool additive)             {}
        public virtual void CurePoison    (int dpr)                            {}
        public virtual void CureAllPoison ()                                   {}


        public virtual void TakeAxeDamage (float factor, int min, int max) { // factor 0.5 - half, 1.0 - all hp
            TakeDamage (min);
        }


        public virtual void TakeGsomDamage () {
            TakeDamage (9999);
        }


        public virtual void ReceiveBlastWave (XY impulse) {
            Velocity += impulse;
        }


    }


}