using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.HomingMissile)]
    public class HomingMissileWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HomingMissile,
                    The.WeaponIcons.HomingMissile
                );
            }
        }

    }

}
