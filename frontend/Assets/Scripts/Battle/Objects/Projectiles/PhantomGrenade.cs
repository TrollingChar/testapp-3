using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class PhantomGrenade : Object {

        private readonly int _seconds;


        public PhantomGrenade (int seconds) {
            _seconds = seconds;
        }


        public override void OnSpawn () {
            var transform = GameObject.transform;
            var assets    = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate (assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3 (0,    30,   0);
            canvas.transform.localScale    =  new Vector3 (0.7f, 0.7f, 1f);
//            canvas.GetComponent<Canvas>().sortingLayerName = "TextFront";

            var timerText =
            UnityEngine.Object.Instantiate (assets.Text, canvas.transform, false).GetComponent <Text> ();

            UnityEngine.Object.Instantiate (assets.PhantomGrenade, transform, false);
            Explosive  = new Explosive25 ();
            Controller = new StandardCtrl ();
            Timer      = new DetonationTimer (new Time {Seconds = _seconds}, timerText);
            // no colliders, no collision handler
        }

    }

}