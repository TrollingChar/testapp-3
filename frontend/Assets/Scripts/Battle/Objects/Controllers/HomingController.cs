﻿using Battle.Objects.Timers;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class HomingController : StandardController {

        private readonly XY _target;


        public HomingController (XY target) {
            _target = target;
            GravityCoeff = 0;
            WaitFlag = true;
            MagnetCoeff = 1; // todo: move this to object itself
            SmokeSize = 20;
            OrientationFlag = true;
        }


        public override void OnAdd () {
            Object.Timer = new CallbackTimer(
                new Time{Seconds = 5},
                () => {
                    Object.Controller = new StandardController {
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

    }

}
