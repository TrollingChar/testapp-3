using Battle.Objects.Controllers;
using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class ExplosivePoison : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealPoisonDamage(3, xy, 30f);
            world.MakePoisonGas(3, xy, 100f);
            var t = PoisonGasController.TimePer1Dmg;
            t.Ticks *= 4;
        }

    }

}
