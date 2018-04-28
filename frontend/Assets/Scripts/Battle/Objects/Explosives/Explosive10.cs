using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive10 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(10, Object.Position, 40f, 20f);
            world.DestroyTerrain(Object.Position, 20f);
            world.SendBlastWave(5f, Object.Position, 40f);
            world.MakeSmoke(Object.Position, 40f);
        }

    }

}
