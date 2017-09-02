using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    [Weapon(WeaponId.Pistol)]
    public class PistolWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Pistol,
                    The<WeaponIcons>.Get().Pistol
                );
            }
        }

    }

}
