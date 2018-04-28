using Battle.Objects.Effects;
using Battle.Objects.Projectiles;
using Core;
using Geometry;
using Utils.Random;


namespace Battle.Objects.Explosives {

    public class ClusterSpawner : Explosive {

        protected override void OnDetonate () {
            for (int i = 0; i < 6; i++) {
                var cluster = new LimonkaCluster();
                var velocity = XY.Polar(8 + RNG.Float() * 8, (RNG.Float() - RNG.Float()) * 0.5f).Rotated90CCW();
                Object.Spawn(cluster, Object.Position, velocity);
//                Object.Spawn(new Explosion(20f), Object.Position);
            }
            The.World.MakeSmoke(Object.Position, 40f);
        }

    }

}
