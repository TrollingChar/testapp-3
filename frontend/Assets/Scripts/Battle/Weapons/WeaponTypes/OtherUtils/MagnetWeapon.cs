using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Magnet)]
    public class MagnetWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Magnet,
                    The<WeaponIcons>.Get().Magnet
                );
            }
        }

    }

}
