using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive40 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(40, Object.Position, 200f, 20f);
            world.DestroyTerrain(Object.Position, 100f);
            world.SendBlastWave(20f, Object.Position, 200f);
            world.MakeSmoke(Object.Position, 200f);
            The.TimerWrapper.Wait(new Time{Seconds = 1});
        }

    }

}
