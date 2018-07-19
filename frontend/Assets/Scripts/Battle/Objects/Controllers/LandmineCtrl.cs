using System;
using System.Collections.Generic;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Projectiles;
using Battle.Objects.Timers;
using Battle.Terrain;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class LandmineCtrl : StandardCtrl {


        private readonly World _world = The.World;
        private Time _control;


        public LandmineCtrl () {
            WaitFlag = true;
        }


        protected override void DoUpdate (TurnData td) {
            base.DoUpdate(td);
            if (Object.Velocity.SqrLength < 1) _control.Ticks++;
            if (_world.Time.Ticks % Time.TPS == 0) {
                if (_control.Seconds >= 0.9f) Object.CollisionHandler = new LandmineStickCH();
                _control.Ticks = 0;
            }
            var mine = (Landmine) Object;
            if (Object.Timer == null && mine.CheckWormsPresence(Landmine.ActivationRadius)) {
                Object.Timer = new LandmineDetonationTimer(new Time{Seconds = 2});
                Wait();
            }
        }

/*
        protected override void DoUpdate (TurnData td) {
            // if stuck and is in open space, unstuck and fall
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
                
                if (Object.Velocity.SqrLength < 1) _control.Ticks++;
                if (The.World.Time.Ticks % Time.TPS == 0) {
                    if (_control.Seconds >= 0.9f) Object.CollisionHandler = new LandmineStickCH();
                    _control.Ticks = 0;
                }
            }
            
            // timer
            if (Timer.Ticks >= 0) {
                Wait();
            } else if (CheckWormsPresence(ActivationRadius)) {
                Activated = true;
                Wait();
            }
            if (TimerText != null) TimerText.text = Activated ? Timer.ToString() : "";
        }
*/

    }

}
