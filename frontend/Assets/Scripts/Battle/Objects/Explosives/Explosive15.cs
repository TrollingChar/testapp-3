﻿using UnityEngine;
using Utils.Singleton;


namespace Battle.Objects.Explosives {

    public class Explosive15 : Explosive {

        protected override void OnDetonate () {
            var world = The<World>.Get();
            world.DealDamage(15, Object.Position, 60f);
            world.DestroyTerrain(Object.Position, 30f);
            world.SendBlastWave(6f, Object.Position, 60f);
        }

    }

}
