using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Battle.Arsenals;
using Battle.Objects;
using Battle.Objects.Controllers;
using Battle.Objects.Effects;
using Battle.Objects.Other;
using Battle.Objects.Projectiles;
using Battle.Teams;
using Battle.Terrain;
using Battle.Terrain.Generation;
using Collisions;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Utils.Danmaku;
using Utils.Messenger;
using Utils.Random;
using Collision = Collisions.Collision;
using Object = Battle.Objects.Object;
using Ray = Battle.Objects.Ray;
using Time = Core.Time;


namespace Battle {

    public partial class World {

        [Obsolete] public const float Precision = 0.1f;
        public readonly LinkedList<Object> Objects;
        
        private float _wind;
        public float Gravity;
        public Land Land;
        public Tiles Tiles;
        public float WaterLevel;
        public Time Time; // world-bound time

        public readonly Messenger<float> OnWindChange;
        
        
        public int Width  { get { return Land.Width; } }
        public int Height { get { return Land.Height; } }
        

        public float Wind {
            get { return _wind; }
            set { OnWindChange.Send(_wind = value); }
        }


        // todo: wrap it in worldgen params
        public World (LandGen gen, LandRenderer renderer) {
            The.World = this;
            OnWindChange = new Messenger<float>();

            Gravity = -Balance.Gravity;
            WaterLevel = 0;
            Tiles = new Tiles();

            Land = new Land(gen, renderer, The.BattleAssets.LandTexture);
            Objects = new LinkedList<Object>();
        }


        public void Update (TurnData td) {
            for (var node = Objects.First; node != null; node = node.Next) {
                node.Value.Update(td);
            }

            PhysicsTick(td);

            // clear all NullObjects WITHOUT INVALIDATING THEIR NODES!
            for (var node = Objects.First; node != null;) {
                if (node.Value is NullObject) {
                    var next = node.Next;
                    Objects.Remove(node);
                    node = next;
                }
                else {
                    node = node.Next;
                }
            }

            Time.Ticks++;
        }


        private void ShowDebugInfo () {
            string s = "";
            var mouseXY = The.Camera.WorldMousePosition;

            int x = Mathf.FloorToInt(mouseXY.X);
            int y = Mathf.FloorToInt(mouseXY.Y);
            s += "pixel: (" + x + ", " + y + ") " + (Land[x, y] == 0 ? "empty" : "solid") + "\n\n";

            int landTileX = Mathf.FloorToInt(mouseXY.X / LandTile.Size);
            int landTileY = Mathf.FloorToInt(mouseXY.Y / LandTile.Size);
            s +=
                "land tile: (" + landTileX + ", " + landTileY + ")\n" +
                "land amount: " + Land.Tiles[landTileX, landTileY].Land + "\n" +
                "vertices count: " + Land.Tiles[landTileX, landTileY].Vertices.Count + "\n" +
                "\n";

            int tileX = Mathf.FloorToInt(mouseXY.X / Tile.Size);
            int tileY = Mathf.FloorToInt(mouseXY.Y / Tile.Size);
            s += "colliders:";
            if (Tiles[tileX, tileY].Colliders.Count == 0) {
                s += " none";
            }
            else {
                foreach (var collider in Tiles[tileX, tileY].Colliders) s += "\n" + collider;
            }

            The.BattleScene.ShowHint(s);
        }



        public Collision CastRay (XY origin, XY direction, float width = 0, float length = 10000) {
            var point = new CircleCollider (origin, width) {Object = new NullObject ()};

            direction.Length = length; // todo: handle infinite length of the ray

            Collision result = null;
            Collision temp;

            foreach (var o in Objects.Where(o => o.Colliders.TrueForAll(c => !c.Overlaps(point))))
            foreach (var c in o.Colliders) {
                temp = point.FlyInto(c, direction);
                if (temp < result) result = temp;
            }

            temp = Land.ApproxCollision(new Circle(origin, width), direction);
            return temp < result ? temp : result;
        }


        public List<Collision> CastUltraRay (XY origin, XY direction, float width = 0, float length = 10000) {
            var point = new CircleCollider (origin, width) {Object = new NullObject ()};

            direction.Length = length; // todo: handle infinite length of the ray

            var result = new List<Collision>();

            foreach (var o in Objects.Where(o => o.Colliders.TrueForAll(c => !c.Overlaps(point)))) {
                // не больше одной коллизии на объект
                Collision min = null;
                foreach (var c in o.Colliders) {
                    var temp = point.FlyInto(c, direction);
                    if (temp < min) min = temp;
                }
                if (min != null) result.Add(min);
            }
            // ультраволны проходят землю насквозь поэтому не кастим к ней
            return result;
        }


        public IEnumerable<Object> CastMelee (XY origin, XY direction) {
            var tester = new MeleeTester();
            Spawn(tester, origin, direction);
            var result = tester.Test();
            tester.Despawn();
            return result;
        }


        public void Spawn (Object o, XY position, XY velocity = default(XY)) {
            o.Node = Objects.AddLast(o);
            o.Position = position;
            o.Velocity = velocity;
            o.GameObject = new GameObject(o.GetType().ToString());
            o.OnSpawn();
            o.UpdateGameObjectPosition();
        }


        private List<XY> GetSpawnPoints () {
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
            var spawnPoints = GetSpawnPoints();
//            Debug.Log(spawnPoints.Count);
            spawnPoints = RNG.PickSome(spawnPoints, players.Count * wormsInTeam);
//            Debug.Log(spawnPoints.Count);

            // teams colors
            var teamColors = TeamColors.Colors.Take(players.Count).ToList();

            // spawn worms
            int currentSpawn = 0;
            var teams = new Dictionary<int, Team>();
            for (int pl = 0; pl < players.Count; pl++) {
                var team = new Team(players[pl], teamColors[pl], new AlphaArsenal());
                for (int w = 0; w < wormsInTeam; w++) {
                    var worm = new Worm();
                    Spawn(worm, spawnPoints[currentSpawn++]);
                    team.AddWorm(worm);
                }
                teams[players[pl]] = team;
            }
            return new TeamManager(teams);
        }


        public void SpawnMines (int count) {
            // todo optimize
            var spawnPoints = GetSpawnPoints().Where(
                xy => {
                    float sqrDist;
                    WormNearestTo(xy, out sqrDist);
                    return sqrDist > 2 * Landmine.ActivationRadius * Landmine.ActivationRadius;
                }
            ).ToList();
            spawnPoints = RNG.PickSome(spawnPoints, count);
            foreach (var point in spawnPoints) Spawn(new Landmine(), point);
        }


        public void DealDamage (int damage, XY center, float radius, float maxDamageRadius) {
            for (var node = Objects.First; node != null; node = node.Next) {
                var obj = node.Value;
                float dist = XY.Distance(center, obj.Position);
                if (dist >= radius) continue;
                obj.TakeDamage(
                    dist > maxDamageRadius
                    ? Mathf.CeilToInt(damage * Mathf.InverseLerp(radius, maxDamageRadius, dist))
                    : damage
                );
            }
        }


        public void DealPoisonDamage (int damage, XY center, float radius) {
            for (var node = Objects.First; node != null; node = node.Next) {
                var obj = node.Value;
                float dist = XY.Distance(center, obj.Position);
                if (dist < radius) obj.AddPoison(damage, false);
            }
        }


        public void DestroyTerrain (XY center, float radius) {
            Land.DestroyTerrain(center, radius);
        }


        public void SendBlastWave (float impulse, XY center, float radius, float offsetY = 0f) {
            // длина вектора считается по center
            // направление по center + offset
            float sqrRadius = radius * radius;
            var waveCenter = center + new XY(0, offsetY);
            for (var node = Objects.First; node != null; node = node.Next) {
                var obj = node.Value;
                float sqrDistance = XY.SqrDistance(center, obj.Position);
                if (sqrDistance >= sqrRadius) continue;

                float currentImpulse = impulse * (1f - Mathf.Sqrt(sqrDistance / sqrRadius));
                obj.ReceiveBlastWave((obj.Position - waveCenter).WithLength(currentImpulse));
            }
        }


        public void MakeSmoke (XY center, float radius) {
            MakeSmoke(center, radius, radius * 0.25f, (int) (radius * 0.5f), radius * 0.5f);
        }


        public void MakeSmoke (XY center, float radius, float size, int count, float flashSize) {
            foreach (var v in Danmaku.Cloud(radius / SmokeCtrl.InvLerpCoeff, count)) {
                Spawn(new Smoke(RNG.Float() * (25f + size)), center, v);
            }
            Spawn(new Flash(flashSize), center);
        }


        public void MakePoisonGas (int damage, XY center, float radius) {
            foreach (var v in Danmaku.Cloud(radius / SmokeCtrl.InvLerpCoeff, (int) (radius * 0.5f))) {
                Spawn(new PoisonGas(RNG.Float() * damage), center, v);
            }
        }


        public void LaunchAirstrike (Func<Object> generator, XY target, bool leftToRight, int bombs = 1) {
            const float vx = 5f;
            float h = Height + Balance.AirplaneExtraHeight;
            float dy = target.Y - h;
            float t = Mathf.Sqrt(2 * dy / Gravity);
            float dx = vx * t;
            if (leftToRight) {
                Spawn(
                    new Airplane(generator, bombs, vx),
                    new XY(target.X - dx - Time.TPS * Balance.AirplaneSpeed, h), // см. AirstrikeTimer
                    new XY(Balance.AirplaneSpeed, 0f)
                );
            }
            else {
                Spawn(
                    new Airplane(generator, bombs, -vx),
                    new XY(target.X + dx + Time.TPS * Balance.AirplaneSpeed, h),
                    new XY(-Balance.AirplaneSpeed, 0f)
                );
            }
        }


        public bool Remove0HpWorms () {
            var worms = Objects
                .OfType<Worm>()
                .Where(w => w.HP <= 0)
                .ToList();
            foreach (var worm in worms) {
//                worm.Detonate();
                worm.Controller = new WormDeathCtrl ();
            }
            return worms.Count > 0;
        }


        public bool AfterTurn () {
            var worms = Objects
                .OfType<Worm>()
                .Where(w => w.Poison > 0)
                .ToList();
            foreach (var worm in worms) worm.TakeDamage(worm.Poison);
            return worms.Count > 0;
        }


        public Worm WormNearestTo (XY xy, out float sqrDist) {
            sqrDist = float.NaN;
            Worm nearest = null;
            foreach (var worm in Objects.OfType<Worm>()) {
                float d2 = XY.SqrDistance(xy, worm.Position);
                if (nearest != null && sqrDist < d2) continue;
                sqrDist = d2;
                nearest = worm;
            }
            return nearest;
        }

    }

}
