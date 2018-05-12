using System;
using Battle.Objects.Controllers;
using Battle.Objects.Timers;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Objects.Other {

    public class Airplane : Object {

        private readonly Func<Object> _generator;
        private readonly int _bombs;
        private readonly float _vx;


        public Airplane (Func<Object> generator, int bombs, float vx) {
            _generator = generator;
            _bombs = bombs;
            _vx = vx;
        }


        public override void OnSpawn () {
            Controller = new AirplaneController();
            Timer = new AirstrikeTimer(_generator, _bombs, _vx);
            UnityEngine.Object.Instantiate(The.BattleAssets.Airplane, GameObject.transform, false)
                .GetComponentInChildren<SpriteRenderer>()
                .flipX = Velocity.X < 0f;
        }


        public override void ReceiveBlastWave (XY impulse) {}

    }

}
