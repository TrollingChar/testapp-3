using Battle.Weapons;
using Core;


namespace Battle.Objects.Crates {

    public class WeaponCrate : Crate {

        private readonly WeaponId _weapon;
        private readonly int _ammo;


        public WeaponCrate (WeaponId id, int ammo = 1) {
            _weapon = id;
            _ammo = ammo;
            var desc = Weapon.DescriptorById(id);
            Text = desc.Name;
            if (ammo < 0) Text += " (беск.)";
            else          Text += " x " + ammo;
        }


        public override void OnAdd () {
            base.OnAdd();
            UnityEngine.Object.Instantiate(The.BattleAssets.WoodenCrate, GameObject.transform, false);
        }


        protected override void OnPickup (Worm worm) {
            worm.Team.Arsenal.AddAmmo(_weapon, _ammo);
        }

    }

}
