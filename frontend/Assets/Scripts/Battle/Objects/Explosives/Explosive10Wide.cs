using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive10Wide : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage(10, Object.Position, 80f, 20f);
            world.DestroyTerrain(Object.Position, 20f);
            world.SendBlastWave(5f, Object.Position, 80f);
            world.MakeSmoke(Object.Position, 80f, 10f, 20, 20f);
        }

    }

}
