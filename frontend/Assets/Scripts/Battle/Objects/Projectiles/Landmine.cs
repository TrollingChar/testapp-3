using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Collisions;
using Core;
using Geometry;
using UnityEngine;
using UnityEngine.UI;
using BoxCollider = Collisions.BoxCollider;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class Landmine : Object {

        public const float Radius = 5f;
        public const float StickCheckRadius = 6f;

        public override void OnAdd () {
            var transform = GameObject.transform;
            var assets = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3(0, 5, 0);
            canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var timerText = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();

            UnityEngine.Object.Instantiate(assets.Landmine, transform, false);
            AddCollider(new CircleCollider(XY.Zero, Radius));
            Explosive = new Explosive25();
            Controller = new LandmineController(timerText);
            CollisionHandler = new LandmineStickCH();
        }


        public override void ReceiveBlastWave (XY impulse) {
            var controller = (LandmineController) Controller;
            controller.Stuck = false;
            base.ReceiveBlastWave(impulse);
        }

    }

}
