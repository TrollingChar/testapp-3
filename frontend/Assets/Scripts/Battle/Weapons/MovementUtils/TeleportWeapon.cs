using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    [Weapon(WeaponId.Teleport)]
    public class TeleportWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Teleport,
                    The<WeaponIcons>.Get().Teleport
                );
            }
        }

    }

}
