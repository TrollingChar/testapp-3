﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Battle.Arsenals;
using Battle.Generation;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Physics;
using Battle.Physics.Collisions;
using Battle.Teams;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Utils.Random;
using Collider = Battle.Physics.Collisions.Collider;
using Collision = Battle.Physics.Collisions.Collision;
using Object = Battle.Objects.Object;
using Ray = Battle.Objects.Ray;


namespace Battle {

    public class World {

        public const float Precision = 0.1f;
        private readonly LinkedList<Object> _objects;

        public float Gravity;
        public Land Land;
        public Tiles Tiles;
        public float WaterLevel;
        public long Time;


        // todo: wrap it in worldgen params
        public World (LandGen gen, LandRenderer renderer) {
            The.World = this;

            Gravity = -0.5f;
            WaterLevel = 0;
            Tiles = new Tiles();
            Time = 0;

            Land = new Land(gen, renderer, The.BattleAssets.LandTexture);
            _objects = new LinkedList<Object>();
        }


        public void Update (TurnData td) {
            if (Time % 500 == 0 && td != null && td.MB) {
            
            }

//            if (Time % 400 == 0 && td != null && td.MB) {
//                Spawn(new Limonka(10), td.XY, new XY(0, 3 + 3 * RNG.Float()).Rotated(RNG.Float() - RNG.Float()));
//            }
//            if (Time % 400 == 200 && td != null && td.MB) {
//                Spawn(new Grenade(10), td.XY, new XY(0, 3 + 3 * RNG.Float()).Rotated(RNG.Float() - RNG.Float()));
//            }

            for (var node = _objects.First; node != null; node = node.Next) {
                node.Value.Update(td);
            }

            PhysicsTick(td);

            // clear all NullObjects WITHOUT INVALIDATING THEIR NODES!
            for (var node = _objects.First; node != null;) {
                if (node.Value is NullObject) {
                    var next = node.Next;
                    _objects.Remove(node);
                    node = next;
                } else {
                    node = node.Next;
                }
            }

            Time += 20;
        }


        [SuppressMessage("ReSharper", "InconsistentNaming")]
        private void PhysicsTick (TurnData td) {
            foreach (var o in _objects) {
                o.Movement = 1;
                o.Excluded.Clear();
                o.ExcludeObjects();
            }

            for (int i = 0, iter = 5; i < iter; i++)
            for (var node = _objects.First; node != null; node = node.Next) {
                var o = node.Value;

                // слишком низкая скорость
                if (o.Velocity.Length * o.Movement <= Settings.PhysicsPrecision) continue;

                var collision = o.NextCollision(o.Movement);
                if (collision != null) collision.ImprovePrecision();
                var o2 = collision == null || collision.Collider2 == null ? null : collision.Collider2.Object;

                if (collision == null) {
                    // нет коллизии
                    o.Position += (o.Movement * o.Velocity).WithLengthReduced(Settings.PhysicsPrecision);
                    o.Movement = 0;
                }
                else if (collision.IsLandCollision) {
                    o.Position += collision.Offset.WithLengthReduced(Settings.PhysicsPrecision);
                    o.Movement -= Mathf.Sqrt(collision.Offset.SqrLength / o.Velocity.SqrLength);
                    var collider1 = collision.Collider1;
                    o.Velocity = Geom.Bounce(
                        o.Velocity,
                        collision.Normal,
                        Mathf.Sqrt(collider1.TangentialBounce * Land.TangentialBounce),
                        Mathf.Sqrt(collider1.NormalBounce * Land.NormalBounce)
                    );
                    o.OnCollision(collision);
                }
                else if (i >= iter - 2 || o.SuperMass < o2.SuperMass) {
                    // если это последняя или предпоследняя итерация, все объекты считаются неподвижными как земля
                    // или если порядок массы первого объекта слишком мал, то тогда то же самое
                    o.Position += collision.Offset.WithLengthReduced(Settings.PhysicsPrecision);
                    o.Movement -= Mathf.Sqrt(collision.Offset.SqrLength / o.Velocity.SqrLength);

                    var invCollision = -collision;
                    
                    if (o2.WillCauseCollision(invCollision)) {
                        var c1 = collision.Collider1;
                        var c2 = collision.Collider2;
                        o.Velocity = Geom.Bounce(
                            o.Velocity,
                            collision.Normal,
                            Mathf.Sqrt(c1.TangentialBounce * c2.TangentialBounce),
                            Mathf.Sqrt(c1.NormalBounce * c2.NormalBounce)
                        );
                    }
                    
                    o.OnCollision(collision);
                    o2.OnCollision(invCollision);
                }
                else {
                    // обычное столкновение объектов с одинаковым порядком массы
                    o.Position += collision.Offset.WithLengthReduced(Settings.PhysicsPrecision);
                    o.Movement -= Mathf.Sqrt(collision.Offset.SqrLength / o.Velocity.SqrLength);
                    
                    // мешает ли второй объект движению первого, если нет то пусть сначала сдвинется он
                    if (XY.Dot(collision.Normal, o2.Velocity) > 0 ||
                        o2.Movement * o2.Velocity.Length <= Settings.PhysicsPrecision
                    ) {
                        var invCollision = -collision;
                        bool owcc = o.WillCauseCollision(collision);
                        bool o2wcc = o2.WillCauseCollision(invCollision);

                        if (owcc || o2wcc) {
                            var velocity
                                = (o.Mass * o.Velocity + o2.Mass * o2.Velocity)
                                / (o.Mass + o2.Mass);
                            var v1 = o.Velocity - velocity;
                            var v2 = o2.Velocity - velocity;
                            var c1 = collision.Collider1;
                            var c2 = collision.Collider2;
                            float tangBounce = Mathf.Sqrt(c1.TangentialBounce * c2.TangentialBounce);
                            float normBounce = Mathf.Sqrt(c1.NormalBounce * c2.NormalBounce);
                            if (o2wcc) o.Velocity = velocity + Geom.Bounce(v1, collision.Normal, tangBounce, normBounce);
                            if (owcc) o2.Velocity = velocity + Geom.Bounce(v2, collision.Normal, tangBounce, normBounce);
                        }
                        
                        o.OnCollision(collision);
                        o2.OnCollision(invCollision);
                    }
                }
                
                if (o.Position.Y < WaterLevel) o.Remove();
            }

            // уменьшить скорость объектов, не потративших очки движения, так как они застряли
            foreach (var o in _objects) {
                o.UpdateGameObjectPosition();
                o.Velocity *= 1 - o.Movement;
            }
        }


        public Collision CastRay (XY origin, XY direction, float width = 0) {
            return null;
            /*
            var point = new CircleCollider(origin, width);
            point.Object = new NullObject();

            direction.Length = 10000; // todo: handle infinite length of the ray

            Collision result = null;

            Collision temp;

            foreach (var o in _objects.Where(o => o.Colliders.TrueForAll(c => !PhysicsCore.Overlap(c, point))))
            foreach (var c in o.Colliders) {
                temp = PhysicsCore.FlyInto(point, c, direction);
                if (temp < result) result = temp;
            }

            temp = null;//Land.CastRay(origin, direction, width);
            return temp < result ? temp : result;*/
        }


        public List<Collision> CastUltraRay (XY origin, XY direction, float width = 0) {
            return new List<Collision>();
            /*
            var point = new CircleCollider(origin, width);
            point.Object = new NullObject();

            direction.Length = 10000; // todo: handle infinite length of the ray

            var result = new List<Collision>();

            foreach (var o in _objects.Where(o => o.Colliders.TrueForAll(c => !PhysicsCore.Overlap(c, point)))) {
                // no more than one collision per object
                Collision min = null;
                foreach (var c in o.Colliders) {
                    var temp = PhysicsCore.FlyInto(point, c, direction);
                    if (temp < min) min = temp;
                }
                if (min != null) result.Add(min);
            }
            // do not cast to land because ultrawave weapons penetrate terrain
            return result;*/
        }


        public void Spawn (Object o, XY position, XY velocity = default(XY)) {
            o.Node = _objects.AddLast(o);
            o.Position = position;
            o.Velocity = velocity;
            o.GameObject = new GameObject(o.GetType().ToString());
            o.OnAdd();
            o.UpdateGameObjectPosition();
        }


        public List<XY> GetSpawnPoints () {
            var result = new List<XY>();
            var rayDirection = new XY(0, Worm.HeadRadius - LandTile.Size * 1.5f);
            for (int x = 0; x < Land.Width / LandTile.Size; x++)
            for (int y = 0; y < Land.Height / LandTile.Size; y++) {
                // valid:
                // . . .
                // . x .
                // # # #
                //   ^--- (x, y)

                if (
                    Land.Tiles[x, y].Land > 0
                    && (Land.Tiles[x - 1, y].Land > 0 || Land.Tiles[x + 1, y].Land > 0)
                    && Land.Tiles[x, y + 1].Land <= 0
                    && (Land.Tiles[x - 1, y + 1].Land <= 0 || Land.Tiles[x + 1, y + 1].Land <= 0)
//                    && Land.Tiles[x, y + 2].Land <= 0
//                    && Land.Tiles[x - 1, y + 2].Land <= 0
//                    && Land.Tiles[x + 1, y + 2].Land <= 0
                ) {
                    var rayOrigin = new XY(x + 0.5f, y + 1f) * LandTile.Size;
                    var collision = new Ray(rayOrigin, new CircleCollider(XY.Zero, Worm.HeadRadius)).Cast(rayDirection);
                    if (collision != null) result.Add(rayOrigin + new XY(0, Worm.BodyHeight));
                }
            }
            return result;
        }


        public TeamManager SpawnTeams (List<int> players, int wormsInTeam) {
            // find valid spawns
            var spawnPoints = RNG.PickSome(GetSpawnPoints(), players.Count * wormsInTeam);

            // teams colors
            var teamColors = TeamColors.Colors.Take(players.Count).ToList();

            // spawn worms
            int currentSpawn = 0;
            var teams = new Dictionary<int, Team>();
            for (int pl = 0; pl < players.Count; pl++) {
                var team = new Team(players[pl], teamColors[pl], new StandardArsenal());
                for (int w = 0; w < wormsInTeam; w++) {
                    var worm = new Worm();
                    Spawn(worm, spawnPoints[currentSpawn++]);
                    team.AddWorm(worm);
                }
                teams[players[pl]] = team;
            }
            return new TeamManager(teams);
        }


        public void DealDamage (int damage, XY center, float radius) {
            float sqrRadius = radius * radius;
            foreach (var o in _objects) {
                float sqrDistance = XY.SqrDistance(center, o.Position);
                if (sqrDistance >= sqrRadius) continue;

                int currentDamage = Mathf.CeilToInt(damage * (1f - Mathf.Sqrt(sqrDistance / sqrRadius)));
                o.GetDamage(currentDamage);
            }
        }


        public void DestroyTerrain (XY center, float radius) {
            Land.DestroyTerrain(center, radius);
        }


        public void SendBlastWave (float impulse, XY center, float radius) {
            float sqrRadius = radius * radius;
            foreach (var o in _objects) {
                float sqrDistance = XY.SqrDistance(center, o.Position);
                if (sqrDistance >= sqrRadius) continue;

                float currentImpulse = impulse * (1f - Mathf.Sqrt(sqrDistance / sqrRadius));
                o.ReceiveBlastWave((o.Position - center).WithLength(currentImpulse));
            }
        }

    }

}
