using Battle.Objects.Projectiles;
using Battle.Objects.Timers;
using Core;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class HomingCtrl : StandardCtrl {

        private readonly XY _target;


        public HomingCtrl (XY target) {
            _target = target;
            GravityCoeff = 0;
            WaitFlag = true;
            MagnetCoeff = 1;
            SmokeSize = 20;
            OrientationFlag = true;
        }


        public override void OnAdd () {
            Object.Timer = new CallbackTimer(
                new Time{Seconds = 5},
                () => {
                    Object.Controller = new StandardCtrl {
                        MagnetCoeff = 1,
                        SmokeSize = 20,
                        OrientationFlag = true
                    };
                    Object.Timer = new DetonationTimer(new Time {Seconds = 20});
                }
            );
        }


        protected override void DoUpdate (TurnData td) {
            var requiredSpeed = (_target - Object.Position).WithLength(20f);
            var acceleration = (requiredSpeed - Object.Velocity).WithLengthClamped(0.5f);
            Object.Velocity += acceleration;
            base.DoUpdate(td);
        }


        public override void OnRemove () {
            var hm = (HomingMissile) Object;
            hm.RemovePointer();
        }

    }

}
