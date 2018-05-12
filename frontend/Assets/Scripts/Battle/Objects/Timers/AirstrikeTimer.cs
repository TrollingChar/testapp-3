using System;
using Core;
using Geometry;


namespace Battle.Objects.Timers {

    public class AirstrikeTimer : Timer {

        private readonly Func<Object> _generator;
        private readonly int _bombs;
        private readonly float _vx;
        private readonly int _t;


        public AirstrikeTimer (Func<Object> generator, int bombs, float vx) : base(new Time{Seconds = 2}) {
            _generator = generator;
            _bombs = bombs;
            _vx = vx;
            _t = Time.TPS - bombs; // если бы летел 4 секунды, было бы 2*TPS, 
        }


        protected override void OnTick () {
            The.BattleScene.Timer.Wait();
            int t = Time.Ticks - _t;
            if (t < 0 || t % 2 != 0 || t / 2 >= _bombs) return;
            The.World.Spawn(_generator(), Object.Position, new XY(_vx, 0f));
        }


        protected override void OnExpire () {
            Object.Despawn();
        }

    }

}
