using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive150 : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage (150, xy, 600f, 20f);
            world.DestroyTerrain (xy, 400f);
            world.SendBlastWave (50f, xy, 600f, -30f);
            world.MakeSmoke (xy, 600f);
            The.Battle.TweenTimer.Wait (new Time {Seconds = 3});
        }

    }

}
