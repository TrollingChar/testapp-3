using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class MeteorExplosive : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage(40, xy, 200f, 35f);
            world.DestroyTerrain(xy, 100f);
            world.SendBlastWave(20f, xy, 200f, -30f);
            world.MakeSmoke(xy, 200f);
            The.TimerWrapper.Wait(new Time{Seconds = 1});
        }

    }

}
