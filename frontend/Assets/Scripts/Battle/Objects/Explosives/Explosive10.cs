using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive10 : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage(10, xy, 40f, 20f);
            world.DestroyTerrain(xy, 20f);
            world.SendBlastWave(5f, xy, 40f);
            world.MakeSmoke(xy, 40f);
        }

    }

}
