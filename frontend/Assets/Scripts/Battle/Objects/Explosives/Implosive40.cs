using Battle.Objects.Controllers;
using Battle.Objects.Effects;
using Core;
using Geometry;
using Utils.Danmaku;
using Utils.Random;


namespace Battle.Objects.Explosives {

    public class Implosive40 : Explosive {

        protected override void OnDetonate (XY xy) {
            var world = The.World;
            world.DealDamage (40, xy, 200f, 20f);
            world.DestroyTerrain (xy, 100f);
            world.SendBlastWave (-20f, xy, 200f, 50f);
            foreach (var v in Danmaku.Cloud (150f, 30)) {
                world.Spawn (new Smoke (RNG.Float () * 55f), xy + v, v.WithLength (-100f / SmokeCtrl.InvLerpCoeff));
            }
            world.Spawn (new Flash (100f), xy);

            The.Battle.TweenTimer.Wait (new Time {Seconds = 1});
        }

    }

}