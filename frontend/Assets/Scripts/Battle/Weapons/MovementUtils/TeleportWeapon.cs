using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class TeleportWeapon {

        
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
