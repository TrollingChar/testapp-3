using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive25 : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage(25, xy, 100f, 20f);
            world.DestroyTerrain(xy, 50f);
            world.SendBlastWave(12.5f, xy, 100f);
            world.MakeSmoke(xy, 100f);
        }

    }

}
