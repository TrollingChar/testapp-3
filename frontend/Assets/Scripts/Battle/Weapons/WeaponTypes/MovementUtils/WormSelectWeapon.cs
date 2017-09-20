using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.WormSelect)]
    public class WormSelectWeapon : StandardWeapon {
        // is it a standard weapon?

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.WormSelect,
                    The<WeaponIcons>.Get().WormSelect
                );
            }
        }

    }

}
