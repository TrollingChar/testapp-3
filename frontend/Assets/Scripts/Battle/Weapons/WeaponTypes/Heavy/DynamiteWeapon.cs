using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Dynamite)]
    public class DynamiteWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Dynamite,
                    The.WeaponIcons.Dynamite
                );
            }
        }

        protected override void OnShoot () {}

    }

}
