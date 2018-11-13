using Core;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Explosives {

    public class ExplosiveNuke : Explosive {

        private XY _offset;

        public ExplosiveNuke (XY offset) { _offset = offset; }


        protected override void OnDetonate (XY xy) {
            xy += _offset;
            var world = The.World;

            float poisonRadius = 1000;
            for (int i = 0; i < world.Objects.Count; i++) {
                var o = world.Objects[i];
                if (o.Despawned) continue;
                float dist = XY.Distance (xy, o.Position);
                if (dist >= poisonRadius) continue;
                o.AddPoison (Mathf.CeilToInt (5 * Mathf.InverseLerp (poisonRadius, 20f, dist)), true);
            }
            
            world.DealDamage (150, xy, 600f, 20f);
            world.DestroyTerrain (xy, 400f);
            world.SendBlastWave (50f, xy, 600f, -30f);
            world.MakeSmoke (xy, 600f);
            The.Battle.TweenTimer.Wait (new Time {Seconds = 3});
        }

    }

}
