using Assets;
using Battle.Objects.CollisionHandlers;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Battle.Physics.Collisions;
using Core;
using Geometry;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.Projectiles {

    public class Grenade : Object {

        private readonly int _timer;


        public Grenade (int timer) {
            _timer = timer;
        }


        public override void OnAdd () {
            var transform = GameObject.transform;
            var assets = The.BattleAssets;

            var canvas = UnityEngine.Object.Instantiate(assets.TopCanvas, transform, false);
            canvas.transform.localPosition += new Vector3(0, 5, 0);
            canvas.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            
            var timerText = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();
            
            UnityEngine.Object.Instantiate(assets.Grenade, transform, false);
            AddCollider(new CircleCollider(XY.Zero, 5f));
            Explosive = new Explosive25();
            Controller = new GrenadeController(_timer * 1000, timerText);
            CollisionHandler = new CollisionHandler();
        }

    }

}
