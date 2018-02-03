using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Core;
using UnityEngine;
using UnityEngine.UI;
using BoxCollider = Collisions.BoxCollider;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class Dynamite : Object {

        public override void OnAdd () {var transform = GameObject.transform;
            var assets = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3(0, 5, 0);
            canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var timerText = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();

            UnityEngine.Object.Instantiate(assets.Dynamite, transform, false);
            AddCollider(new BoxCollider(-2, 2, -4, 4));
            Explosive = new Explosive40();
            Controller = new GrenadeController(new Time {Seconds = 6}, timerText);
            CollisionHandler = new DynamiteCollisionHandler();
        }

    }

}
