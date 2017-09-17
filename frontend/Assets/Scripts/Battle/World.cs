using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Assets;
using Battle.Arsenals;
using Battle.Generation;
using Battle.Objects;
using Battle.Physics;
using Battle.Physics.Collisions;
using Battle.State;
using Battle.Teams;
using Geometry;
using UnityEngine;
using Utils.Random;
using Utils.Singleton;
using Object = Battle.Objects.Object;
using Ray = Battle.Objects.Ray;


namespace Battle {

    public class World {

        public const float Precision = 0.1f;
        private readonly LinkedList<Object> _objects;

        private readonly GameStateController _state;

        public float Gravity;
        public Land Land;
        public Tiles Tiles;
        public float WaterLevel;

        // todo: wrap it in worldgen params
        public World (LandGen gen, SpriteRenderer renderer) {
            The<World>.Set(this);

            _state = The<GameStateController>.Get();

            Gravity = -0.5f;
            WaterLevel = 0;
            Tiles = new Tiles();

            Land = new Land(gen, The<BattleAssets>.Get().LandTexture, renderer);
            _objects = new LinkedList<Object>();
        }


        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public void Update (TurnData td) {
            if (_state.Timer % 500 == 0 && td != null && td.MB) {
                // ???
            }

            foreach (var o in _objects) o.Update(td);
            foreach (var o in _objects) {
                o.Movement = 1;
                o.Excluded.Clear();
                o.ExcludeObjects();
            }

            for (int i = 0, iter = 5; i < iter; i++) {
                foreach (var o in _objects) {
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
                        o.Velocity = Geom.Bounce(
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
                            o.Velocity = Geom.Bounce(
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
                            o.Velocity = Geom.Bounce(
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
                                if (o2wcc) o.Velocity = velocity + Geom.Bounce(v1, c.Normal, tangBounce, normBounce);
                                if (owcc) o2.Velocity = velocity + Geom.Bounce(v2, c.Normal, tangBounce, normBounce);
                            }
                            o.OnCollision(c);
                            o2.OnCollision(-c);
                        }
                    }
                    if (o.Position.Y < WaterLevel) o.Remove();
                }
            }

            // handle stuck objects:
            foreach (var o in _objects) {
                o.UpdateSpritePosition();
                o.Velocity *= 1 - o.Movement;
            }

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
        }


        public void AddObject (Object o, XY position, XY velocity = default(XY)) {
            o.Node = _objects.AddLast(o);
            o.Position = position;
            o.Velocity = velocity;
            o.OnAdd();
            o.UpdateSpritePosition();
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
                    AddObject(worm, spawnPoints[currentSpawn++]);
                    team.AddWorm(worm);
                }
                teams[players[pl]] = team;
            }
            return new TeamManager(teams);
        }

    }

}
