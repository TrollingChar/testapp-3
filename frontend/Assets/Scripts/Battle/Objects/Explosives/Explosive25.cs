﻿using UnityEngine;
using Utils.Singleton;

namespace Battle.Objects.Explosives {
    public class Explosive25 : Explosive
    {
        protected override void OnDetonate() {
            Debug.Log("типа взрыв, средний, урон 25");
            World world = The<World>.Get();
            world.DealDamage(25, Object.Position, 80f);
            world.DestroyTerrain(Object.Position, 40f);
        }
    }
}