using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    [Weapon(WeaponId.PoisonArrow)]
    public class PoisonArrowWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The<WeaponIcons>.Get().PoisonArrow
                );
            }
        }

    }

}
