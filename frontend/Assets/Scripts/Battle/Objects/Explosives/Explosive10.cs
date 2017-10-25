using UnityEngine;
using Utils.Singleton;


namespace Battle.Objects.Explosives {

    public class Explosive10 : Explosive {

        protected override void OnDetonate () {
            var world = The<World>.Get();
            world.DealDamage(10, Object.Position, 30f);
            world.DestroyTerrain(Object.Position, 15f);
            world.SendBlastWave(4f, Object.Position, 30f);
        }

    }

}
