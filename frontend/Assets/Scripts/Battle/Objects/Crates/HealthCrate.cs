using Core;


namespace Battle.Objects.Crates {

    public class HealthCrate : Crate {

        private readonly int _hp;


        public HealthCrate (int hp) {
            _hp = hp;
            Text = "+" + hp;
        }
        
        
        public override void OnSpawn () {
            base.OnSpawn();
            UnityEngine.Object.Instantiate(The.BattleAssets.HealthCrate, GameObject.transform, false);
        }


        protected override void OnPickup (Worm worm) {
            worm.CureAllPoison();
            worm.TakeHealing(_hp, false);
        }

    }

}
