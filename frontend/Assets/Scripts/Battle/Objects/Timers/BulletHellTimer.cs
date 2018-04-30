using Core;
using Geometry;
using UnityEngine;
using Utils.Random;
using Time = Core.Time;


namespace Battle.Objects.Timers {

    public class BulletHellTimer : Timer {

        private readonly World _world = The.World;

        public BulletHellTimer () : base(new Time {Seconds = 5}) {}


        protected override void OnExpire () {
            Object.Despawn();
        }


        protected override void OnTick () {
            for (int i = 0; i < 10; i++) {
                var target = new XY(RNG.Float() * _world.Width, RNG.Float() * _world.Height);
                var direction = XY.Polar(4000f, (RNG.Float() * 0.5f - 0.75f) * Mathf.PI);
                var rayOrigin = target - direction;
                
                var collision = The.World.CastRay(rayOrigin, direction);
                if (collision == null) return;
                if (collision.Collider2 == null) {
                    The.World.DestroyTerrain(rayOrigin + collision.Offset, 5f);
                }
                else {
                    var o = collision.Collider2.Object;
                    o.TakeDamage(1);
                    o.ReceiveBlastWave(direction.WithLength(2f));
                }
            }
        }

    }

}
