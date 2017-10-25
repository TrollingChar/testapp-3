using UnityEngine;
using Utils.Singleton;


namespace Battle.Objects.Explosives {

    public class Explosive40 : Explosive {

        protected override void OnDetonate () {
            var world = The<World>.Get();
            world.DealDamage(40, Object.Position, 120f);
            world.DestroyTerrain(Object.Position, 60f);
            world.SendBlastWave(15f, Object.Position, 120f);
        }

    }

}
