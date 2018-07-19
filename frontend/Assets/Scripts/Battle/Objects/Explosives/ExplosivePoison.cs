using Battle.Objects.Controllers;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class ExplosivePoison : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealPoisonDamage(3, xy, 30f);
            world.MakePoisonGas(3, xy, 100f);
            var t = PoisonGasCtrl.TimePer1Dmg;
            t.Ticks *= 4;
        }

    }

}
