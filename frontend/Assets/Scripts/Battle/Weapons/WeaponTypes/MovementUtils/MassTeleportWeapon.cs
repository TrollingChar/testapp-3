using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.MassTeleport)]
    public class MassTeleportWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MassTeleport,
                    The<WeaponIcons>.Get().MassTeleport
                );
            }
        }

    }

}
