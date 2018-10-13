using Battle.Experimental;
using Battle.Objects.Projectiles;
using Battle.State;
using Core;
using Geometry;
using Utils.Danmaku;
using Utils.Random;
using Time = Core.Time;


namespace Battle.Objects.Timers {

    public class ArmageddonTimer : Timer {

        private readonly World _world = The.World;


        public ArmageddonTimer () : base (new Time {Seconds = 20f}) {}


        protected override void OnExpire () {
            Object.Despawn ();
        }


        protected override void OnTick () {
            The.Battle.TweenTimer.Wait ();
            if (Time.Ticks % 10 != 0) return;

            var position = new XY (RNG.Float () * (_world.Width + 2000f) - 1000f, _world.Height + 1000f);
            var velocity = Danmaku.ShotgunBullet (XY.Down, 2f, 15f, 30f);
            _world.Spawn (new Meteor (), position, velocity);
        }

    }

}