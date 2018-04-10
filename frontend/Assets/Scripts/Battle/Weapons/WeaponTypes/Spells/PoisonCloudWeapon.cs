using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.PoisonCloud)]
    public class PoisonCloudWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonCloud,
                    The.WeaponIcons.PoisonCloud,
                    "ядовитое облако"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
