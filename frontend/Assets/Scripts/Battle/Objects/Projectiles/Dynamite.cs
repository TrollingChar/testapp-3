using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Objects.Timers;
using Core;
using UnityEngine;
using UnityEngine.UI;
using BoxCollider = Collisions.BoxCollider;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class Dynamite : Object {

        public override void OnSpawn () {var transform = GameObject.transform;
            var assets = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3(0f, 5f, 0f);
            canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
//            canvas.GetComponent<Canvas>().sortingLayerName = "TextFront";

            var timerText = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();

            UnityEngine.Object.Instantiate(assets.Dynamite, transform, false);
            AddCollider(new BoxCollider(-4f, 4f, -6f, 6f));
            Explosive = new Explosive40();
            Controller = new StandardController();
            Timer = new DetonationTimer(new Time {Seconds = 6f}, timerText);
            CollisionHandler = new DynamiteCH();
        }

    }

}
