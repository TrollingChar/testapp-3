using Core;


namespace Battle.Objects.Explosives {

    public class Explosive10Wide : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(10, Object.Position, 80f);
            world.DestroyTerrain(Object.Position, 20f);
            world.SendBlastWave(4f, Object.Position, 80f);
        }

    }

}
