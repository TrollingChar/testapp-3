using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Effects;
using Battle.Objects.Explosives;
using Battle.State;
using Core;
using Geometry;
using UnityEngine;
using BoxCollider = Collisions.BoxCollider;


namespace Battle.Objects.Other.Crates {

    public class Crate : Object {

        public const float Size = 30;
        private bool _collected;
        private GameStateController _state;
        private ActiveWorm _activeWorm;
        public string Text { get; protected set; }


        public Crate () : base (60, 2) {}
        

        public override void OnSpawn () {
            _state = The.GameState;
            _activeWorm = The.ActiveWorm;
            AddCollider(new BoxCollider(Size * -0.5f, Size * 0.5f, Size * -0.5f, Size * 0.5f));
            Controller = new CrateControllerFall();
            CollisionHandler = new CrateCH();
            Explosive = new Explosive25();
        }


        protected override bool PassableFor (Object o) {
            if (this == o) return true;
            var ray = o as Ray;
            if (ray != null) o = ray.Object;
            return The.ActiveWorm.Is(o) && The.GameState.CurrentState == GameState.Turn;
        }


        public override void TakeDamage (int damage) {
            Detonate();
        }


        public void CollectBy (Worm worm) {
            if (_collected) return;
            _collected = true;
            Despawn();
            Spawn(new Label(Text, Color.white, 1f, new LabelControllerRise()), Position, new XY(0f, 10f));
            OnPickup(worm);
        }


        protected virtual void OnPickup (Worm worm) {
        }


        public override void ReceiveBlastWave (XY impulse) {
            Controller = new CrateControllerFall();
            base.ReceiveBlastWave(impulse);
        }


        public void CheckIfCollected () {
            if (_state.CurrentState != GameState.Turn) return;
            var worm = _activeWorm.Worm;
            if (worm == null) return;
            foreach (var wormCollider in worm.Colliders)
            foreach (var crateCollider in Colliders) {
                if (!wormCollider.Overlaps(crateCollider)) continue;
                CollectBy(worm);
                return;
            }
        }

    }

}
