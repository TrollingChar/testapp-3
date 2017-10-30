using UnityEngine;
using Utils.Singleton;


namespace Battle.Objects.Explosives {

    public class Explosive40 : Explosive {

        protected override void OnDetonate () {
            var world = The<World>.Get();
            world.DealDamage(40, Object.Position, 160f);
            world.DestroyTerrain(Object.Position, 80f);
            world.SendBlastWave(15f, Object.Position, 160f);
        }

    }

}
