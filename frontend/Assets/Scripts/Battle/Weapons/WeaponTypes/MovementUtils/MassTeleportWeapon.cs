using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.MassTeleport)]
    public class MassTeleportWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MassTeleport,
                    The.WeaponIcons.MassTeleport
                );
            }
        }

    }

}
