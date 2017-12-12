using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Mole)]
    public class MoleWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Mole,
                    The.WeaponIcons.Mole
                );
            }
        }

        protected override void OnShoot () {}

    }

}
