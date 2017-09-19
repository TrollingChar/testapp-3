using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Parachute)]
    public class ParachuteWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Parachute,
                    The<WeaponIcons>.Get().Parachute
                );
            }
        }

    }

}
