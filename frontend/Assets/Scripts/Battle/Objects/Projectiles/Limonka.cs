using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Collisions;
using Core;
using Geometry;
using UnityEngine;
using UnityEngine.UI;
using Time = Core.Time;


namespace Battle.Objects.Projectiles {

    public class Limonka : Object {

        private readonly int _seconds;


        public Limonka (int seconds) {
            _seconds = seconds;
        }


        public override void OnAdd () {
            var transform = GameObject.transform;
            var assets = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3(0, 5, 0);
            canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);

            var timerText = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();

            UnityEngine.Object.Instantiate(assets.Limonka, transform, false);

//            AddCollider(new BoxCollider(-5, 5, -5, 5));
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new ClusterSpawner();
            Controller = new GrenadeController(new Time {Seconds = _seconds}, timerText);
            CollisionHandler = new CollisionHandler();
        }

    }

}
