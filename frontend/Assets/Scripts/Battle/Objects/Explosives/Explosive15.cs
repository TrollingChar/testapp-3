using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive15 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(15, Object.Position, 60f);
            world.DestroyTerrain(Object.Position, 30f);
            world.SendBlastWave(6f, Object.Position, 60f);
//            Object.Spawn(new Explosion(30f), Object.Position);
            The.World.MakeSmoke(Object.Position, 30f);
        }

    }

}
