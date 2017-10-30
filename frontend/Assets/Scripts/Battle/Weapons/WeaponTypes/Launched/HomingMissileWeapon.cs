using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.HomingMissile)]
    public class HomingMissileWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HomingMissile,
                    The<WeaponIcons>.Get().HomingMissile
                );
            }
        }

    }

}
