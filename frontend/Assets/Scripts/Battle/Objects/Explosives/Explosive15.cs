﻿using Battle.Objects.Effects;
using Core;


namespace Battle.Objects.Explosives {

    public class Explosive15 : Explosive {

        protected override void OnDetonate () {
            var world = The.World;
            world.DealDamage(15, Object.Position, 60f, 20f);
            world.DestroyTerrain(Object.Position, 30f);
            world.SendBlastWave(7.5f, Object.Position, 60f);
            world.MakeSmoke(Object.Position, 60f);
        }

    }

}
