using Battle.Objects.Controllers;
using Core;


namespace Battle.Objects.Effects {

    public class Smoke : Effect {

        private readonly float _size;


        public Smoke (float size) {
            _size = size;
        }


        public override void OnSpawn () {
            UnityEngine.Object.Instantiate(The.BattleAssets.Smoke, GameObject.transform, false);
            Controller = new SmokeController(_size);
        }

    }

}
