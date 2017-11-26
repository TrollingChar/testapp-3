using System.Collections.Generic;
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
//            if (_state.Timer % 500 == 0 && td != null && td.MB) {
            
//            }

            if (Time % 100 == 0 && td != null && td.MB) {
                Spawn(new Grenade(5), td.XY, new XY(0, 3 + 3 * RNG.Float()).Rotated(RNG.Float() - RNG.Float()));
            }

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

                if (o.Velocity.Length * o.Movement <= Precision) continue;

                var c = o.NextCollision(o.Movement);
                var o2 = c == null || c.Collider2 == null
                    ? null
                    : c.Collider2.Object;

                if (c == null) {
                    // no collision
                    o.Position += o.Movement * (1 - Precision) * o.Velocity;
                    o.Movement = 0;

                } else if (c.Collider2 == null) {
                    // collided with land
                    o.Position += c.Offset.WithLengthReduced(Precision);
                    o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                    o.Velocity = OldGeom.Bounce(
                        o.Velocity,
                        c.Normal,
                        Mathf.Sqrt(c.Collider1.TangentialBounce * Land.TangentialBounce),
                        Mathf.Sqrt(c.Collider1.NormalBounce * Land.NormalBounce)
                    );
                    o.OnCollision(c);

                } else if (i > iter - 3 || o.SuperMass < o2.SuperMass) {
                    // force hard collision as if second object was land
                    // as long as magic number equals to 3, the eternal balance in the world will remain
                    o.Position += c.Offset.WithLengthReduced(Precision);
                    o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                    bool o2wcc = o2.WillCauseCollision(-c);
                    if (o2wcc) {
                        o.Velocity = OldGeom.Bounce(
                            o.Velocity,
                            c.Normal,
                            Mathf.Sqrt(c.Collider1.TangentialBounce * c.Collider2.TangentialBounce),
                            Mathf.Sqrt(c.Collider1.NormalBounce * c.Collider2.NormalBounce)
                        );
                    }
                    o.OnCollision(c);
                    o2.OnCollision(-c);

                } else if (o2.SuperMass < o.SuperMass) {
                    // treat first object as it has infinite mass
                    o.Position += c.Offset.WithLengthReduced(Precision);
                    o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);
                    bool o2wcc = o2.WillCauseCollision(-c);
                    if (o2wcc) {
                        o.Velocity = OldGeom.Bounce(
                            o.Velocity,
                            c.Normal,
                            Mathf.Sqrt(c.Collider1.TangentialBounce * c.Collider2.TangentialBounce),
                            Mathf.Sqrt(c.Collider1.NormalBounce * c.Collider2.NormalBounce)
                        );
                    }
                    o.OnCollision(c);
                    o2.OnCollision(-c);

                } else {
                    o.Position += c.Offset.WithLengthReduced(Precision);
                    o.Movement -= Mathf.Sqrt(c.Offset.SqrLength / o.Velocity.SqrLength);

                    // найти скалярное произведение нормали столкновения и скорости второго объекта
                    // так мы узнаем мешает ли он движению первого
                    float temp = XY.Dot(c.Normal, c.Collider2.Object.Velocity);
                    if (
                        temp >= 0
                        || c.Collider2.Object.Movement * c.Collider2.Object.Velocity.Length <= Precision
                    ) {
                        // collision
                        bool owcc = o.WillCauseCollision(c);
                        bool o2wcc = o2.WillCauseCollision(-c);
                        if (owcc || o2wcc) {
                            var velocity = (o.Mass * o.Velocity + o2.Mass * o2.Velocity) / (o.Mass + o2.Mass);
                            var v1 = o.Velocity - velocity;
                            var v2 = o2.Velocity - velocity;
                            float tangBounce = Mathf.Sqrt(
                                c.Collider1.TangentialBounce * c.Collider2.TangentialBounce
                            );
                            float normBounce = Mathf.Sqrt(
                                c.Collider1.NormalBounce * c.Collider2.NormalBounce
                            );
                            if (o2wcc) o.Velocity = velocity + OldGeom.Bounce(v1, c.Normal, tangBounce, normBounce);
                            if (owcc) o2.Velocity = velocity + OldGeom.Bounce(v2, c.Normal, tangBounce, normBounce);
                        }
                        o.OnCollision(c);
                        o2.OnCollision(-c);
                    }
                }
                if (o.Position.Y < WaterLevel) o.Remove();
            }

            // handle stuck objects:
            foreach (var o in _objects) {
                o.UpdateGameObjectPosition();
                o.Velocity *= 1 - o.Movement;
            }
        }


        public Collision CastRay (XY origin, XY direction, float width = 0) {
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
            return temp < result ? temp : result;
        }


        public List<Collision> CastUltraRay (XY origin, XY direction, float width = 0) {
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
            return result;
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
