using Battle.Objects.Projectiles;
using Core;
using Geometry;
using Utils.Danmaku;


namespace Battle.Objects.Explosives {

    public class ClusterSpawner : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            foreach (var v in Danmaku.Shotgun(XY.Up, 0.5f, 8, 16, 6)) {
                world.Spawn(new LimonkaCluster(), xy, v);
            }
            world.MakeSmoke(xy, 40f);
        }

    }

}
