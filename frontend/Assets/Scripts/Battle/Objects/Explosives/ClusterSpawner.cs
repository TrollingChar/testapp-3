﻿using Battle.Objects.Projectiles;
using Geometry;
using UnityEngine;
using Utils.Random;
using Utils.Singleton;

namespace Battle.Objects.Explosives
{
    public class ClusterSpawner : Explosive
    {
        protected override void OnDetonate()
        {
            for (int i = 0; i < 6; i++) {
                var cluster = new LimonkaCluster();
                XY velocity = XY.FromPolar(8 + RNG.Float() * 8, (RNG.Float() - RNG.Float()) * 0.5f).Rotated90CCW();
                Object.Spawn(cluster, Object.Position, velocity);
            }
        }
    }
}