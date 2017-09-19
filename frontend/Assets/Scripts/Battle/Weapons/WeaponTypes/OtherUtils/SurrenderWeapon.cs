using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Surrender)]
    public class SurrenderWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Surrender,
                    The<WeaponIcons>.Get().Surrender
                );
            }
        }

    }

}
