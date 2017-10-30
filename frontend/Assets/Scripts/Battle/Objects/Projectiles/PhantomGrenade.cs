using Assets;
using Battle.Objects.Controllers;
using Battle.Objects.Explosives;
using Utils.Singleton;


namespace Battle.Objects.Projectiles {

    public class PhantomGrenade : Object {

        private readonly int _timer;


        public PhantomGrenade (int timer) {
            _timer = timer;
        }


        public override void OnAdd () {
            UnityEngine.Object.Instantiate(The<BattleAssets>.Get().PhantomGrenade, GameObject.transform, false);
            Explosive = new Explosive25();
            Controller = new GrenadeController(_timer * 1000);
            // no colliders, no collision handler
        }

    }

}
