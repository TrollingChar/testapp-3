﻿using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Collisions;
using Core;
using Geometry;


namespace Battle.Objects.Projectiles {

    public class PlasmaBall : Object {

        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The.BattleAssets.PlasmaBall, GameObject.transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new StandardController {
                WindCoeff = 0.1f,
                MagnetCoeff = 1
            };
            Timer = new DetonationTimer(new Time{Seconds = 20});
            CollisionHandler = new DetonatorCollisionHandler();
        }

    }

}
