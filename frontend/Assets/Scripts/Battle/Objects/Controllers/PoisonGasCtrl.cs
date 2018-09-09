using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class PoisonGasCtrl : Controller {

        public const float InvLerpCoeff = 10;
        private readonly World _world = The.World;

        private Time _time;
        public static readonly Time TimePer1Dmg = new Time {Seconds = 0.5f};
        private const float RadiusPer1Dmg = 15f;


        public PoisonGasCtrl (float damage) {
            _time.Ticks = Mathf.CeilToInt(damage * TimePer1Dmg.Ticks);
        }


        public override void OnAdd () {
            float r = RadiusPer1Dmg * _time.Ticks / TimePer1Dmg.Ticks;
            Object.GameObject.transform.localScale = new Vector3(r, r, 1);
        }


        protected override void DoUpdate (TurnData td) {
            Object.Velocity = XY.Lerp(Object.Velocity, new XY(_world.Wind, 0.5f), 1 / InvLerpCoeff);
            float r = RadiusPer1Dmg * _time.Ticks / TimePer1Dmg.Ticks;
            _world.DealPoisonDamage(
                Mathf.CeilToInt((float) _time.Ticks / TimePer1Dmg.Ticks),
                Object.Position,
                r + 15f // todo: use colliders
            );
            if (--_time.Ticks <= 0) {
                Object.Despawn();
            }
            else {
                r = RadiusPer1Dmg * _time.Ticks / TimePer1Dmg.Ticks;
                Object.GameObject.transform.localScale = new Vector3(r, r, 1);
            }
            Wait();
        }

    }

}
