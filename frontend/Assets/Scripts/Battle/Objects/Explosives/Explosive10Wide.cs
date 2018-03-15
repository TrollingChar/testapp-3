using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive10Wide : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(10, Object.Position, 80f);
            world.DestroyTerrain(Object.Position, 20f);
            world.SendBlastWave(5f, Object.Position, 80f);
//            Object.Spawn(new Explosion(30f), Object.Position);
            The.World.MakeSmoke(Object.Position, 20f);
        }

    }

}
