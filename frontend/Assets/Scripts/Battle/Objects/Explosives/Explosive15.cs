using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive15 : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage(15, xy, 60f, 20f);
            world.DestroyTerrain(xy, 30f);
            world.SendBlastWave(7.5f, xy, 60f);
            world.MakeSmoke(xy, 60f);
        }

    }

}
