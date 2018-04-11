using Core;


namespace Battle.Objects.Crates {

    public class HealthCrate : Crate {

        private readonly int _hp;


        public HealthCrate (int hp) {
            _hp = hp;
            Text = "+" + hp;
        }
        
        
        public override void OnAdd () {
            base.OnAdd();
            UnityEngine.Object.Instantiate(The.BattleAssets.HealthCrate, GameObject.transform, false);
        }


        protected override void OnPickup (Worm worm) {
            worm.TakeHealing(_hp, false);
        }

    }

}
