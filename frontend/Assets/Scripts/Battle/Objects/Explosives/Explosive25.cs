using Battle.Objects.Effects;
using Core;
using Geometry;


namespace Battle.Objects.Explosives {

    public class Explosive25 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(25, Object.Position, 100f);
            world.DestroyTerrain(Object.Position, 50f);
            world.SendBlastWave(12.5f, Object.Position, 100f);
//            Object.Spawn(new Explosion(50f), Object.Position);
            The.World.MakeSmoke(Object.Position, 50f);
        }

    }

}
