using System;
using Battle.Objects.Timers;
using Collisions;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Controllers {

    public class WormDeathCtrl : Controller {

        public override void OnAdd () {
            Object.Velocity = XY.Zero;
//            Object.Immobile = true;
            Object.Timer = new DetonationTimer (new Time {Seconds = 1.5f});
            ((Worm) Object).NewWormGO.Die();
        }


        protected override void DoUpdate (TurnData td) {
            var worm = (Worm) Object;

            worm.Name = "death";
/* стоит ли оставлять вот это все?
            var collision =
            new Ray (worm.Tail.Center, new CircleCollider (XY.Zero, Worm.HeadRadius)) {Object = Object}.Cast (
                new XY (0f, -Worm.MaxDescend)
            );

            // will fall?
            if (collision == null) {
                Object.Controller = new WormJumpCtrl ();
                return;
            }
*/
            Wait ();
        }


        public override void OnRemove () {
            Debug.LogError ("can't remove that controller!");
        }

    }

}
