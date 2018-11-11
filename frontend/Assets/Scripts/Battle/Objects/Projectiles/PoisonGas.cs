using Battle.Objects.Controllers;
using Core;


namespace Battle.Objects.Projectiles {

    public class PoisonGas : Object {

        private readonly float _damage;


        public PoisonGas (float damage) {
            _damage = damage;
        }


        public override void OnSpawn () {
            UnityEngine.Object.Instantiate (The.BattleAssets.PoisonGas, GameObject.transform, false);
            Controller = new PoisonGasCtrl (_damage);
        }

    }

}