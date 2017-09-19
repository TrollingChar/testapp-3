using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Dynamite)]
    public class DynamiteWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Dynamite,
                    The<WeaponIcons>.Get().Dynamite
                );
            }
        }

    }

}
