using Battle.Objects.Controllers;
using Battle.Objects.Timers;
using Core;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Objects.Effects {

    public class Flash : Effect {

        private readonly float _size;


        public Flash (float size) {
            _size = size;
        }


        public override void OnSpawn () {
            var ob = UnityEngine.Object.Instantiate(The.BattleAssets.Flash, GameObject.transform, false);
            ob.transform.localScale = new Vector3(_size, _size, 1);
            ob.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 0.5f);
            Timer = new DetonationTimer(new Time{Seconds = 0.1f});
//            Controller = new DisappearingController();
        }

    }

}
