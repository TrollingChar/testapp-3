using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    [Weapon(WeaponId.UltraRifle)]
    public class UltraRifleWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.UltraRifle,
                    The<WeaponIcons>.Get().UltraRifle
                );
            }
        }

    }

}
