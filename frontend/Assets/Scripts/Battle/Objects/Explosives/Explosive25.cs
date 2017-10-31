using Core;


namespace Battle.Objects.Explosives {

    public class Explosive25 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(25, Object.Position, 100f);
            world.DestroyTerrain(Object.Position, 50f);
            world.SendBlastWave(10f, Object.Position, 100f);
        }

    }

}
