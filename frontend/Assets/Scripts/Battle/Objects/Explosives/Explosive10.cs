using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive10 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(10, Object.Position, 40f);
            world.DestroyTerrain(Object.Position, 20f);
            world.SendBlastWave(4f, Object.Position, 40f);
//            Object.Spawn(new Explosion(20f), Object.Position);
            The.World.MakeSmoke(Object.Position, 20f);
        }

    }

}
