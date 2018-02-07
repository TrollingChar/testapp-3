using System.Collections.Generic;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Projectiles;
using Battle.Terrain;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.Controllers {

    public class LandmineController : StandardController {

        public Text TimerText { get; private set; }

        private bool _activated;
        private readonly World _world;
        private bool _stuck;


        public bool Activated {
            get { return _activated; }
            set {
                _activated = value;
                Timer.Seconds = 2;
            }
        }

        public bool Stuck {
            get { return _stuck; }
            set {
                _stuck = value;
//                if (!value) Object.CollisionHandler = new CollisionHandler();
            }
        }


        public LandmineController (Text timerText) {
            _world = The.World;
            TimerText = timerText;
            Timer.Seconds = 2;
        }


        protected override void DoUpdate (TurnData td) {
            if (Stuck) {
                var collider = new CircleCollider(XY.Zero, Landmine.StickCheckRadius);
                Object.AddCollider(collider);

                var box = collider.AABB;
                var tiles = box.ToTiles(Tile.Size);
                
                for (int x = tiles.Left; x < tiles.Right; x++)
                for (int y = tiles.Bottom; y < tiles.Top; y++) {
                    foreach (var c in _world.Tiles[x, y].Colliders) {
                        if (collider.Object == Object) continue;
                        if (collider.Overlaps(c)) goto found;
                    }
                }
                const float rr = Landmine.StickCheckRadius * Landmine.StickCheckRadius;
                for (int x = Mathf.FloorToInt(box.Left); x < Mathf.CeilToInt(box.Right); x++)
                for (int y = Mathf.FloorToInt(box.Bottom); y < Mathf.CeilToInt(box.Top); y++) {
                    // check center of tile
                    if (_world.Land[x, y] != 0 && XY.SqrDistance(new XY(x + 0.5f, y + 0.5f), Object.Position) < rr) {
                        goto found;
                    }
                }
                Stuck = false;
//                Object.Velocity = XY.Zero;
            found:
                // do nothing
                Object.RemoveCollider(collider);
            }
            if (!Stuck) {
                Object.Velocity.Y += _world.Gravity;
                Wait();
            }
            if (Timer.Ticks >= 0) {
                Wait();
            } else if (CheckWormsPresence(40)) {
                Activated = true;
                Wait();
            }
            if (TimerText != null) TimerText.text = Activated ? Timer.ToString() : "";
        }


        public override void OnTimer () {
            if (Activated) Object.Detonate();
        }


        private bool CheckWormsPresence (float radius) {
            return XY.SqrDistance(Object.Position, _world.WormNearestTo(Object.Position).Position) <= radius * radius;
        }

    }

}
