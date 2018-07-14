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

    public class LandmineControllerStuck : Controller {

        private readonly World _world = The.World;


        public override void OnAdd () {
            Object.Immobile = true;
        }


        public override void OnRemove () {
            Object.Immobile = false;
        }


        protected override void DoUpdate (TurnData td) {
            var collider = new CircleCollider(XY.Zero, Landmine.StickCheckRadius);
            Object.AddCollider(collider);

            var box = collider.AABB;
            var tiles = box.ToTiles(Tile.Size);

            for (int x = tiles.Left; x < tiles.Right; x++)
            for (int y = tiles.Bottom; y < tiles.Top; y++) {
                foreach (var c in _world.Tiles[x, y].Colliders) {
                    if (c.Object == Object) continue;
                    if (c.Overlaps(collider)) goto found;
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
            Object.Controller = new LandmineController();
//            Object.CollisionHandler = new CollisionHandler();
            found:
            // do nothing
            Object.RemoveCollider(collider);

            var mine = (Landmine) Object;
            if (Object.Timer == null && mine.CheckWormsPresence(Landmine.ActivationRadius)) {
                Object.Timer = new LandmineDetonationTimer(new Time{Seconds = 2});
                Wait();
            }
        }

    }

}
