using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Superfrog)]
    public class SuperfrogWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Superfrog,
                    The.WeaponIcons.Superfrog
                );
            }
        }

        protected override void OnShoot () {}

    }

}
