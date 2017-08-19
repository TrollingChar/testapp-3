using Assets;
using Utils.Singleton;


namespace War.Weapons.MovementUtils {

    public class TeleportWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Teleport,
                    The<WeaponIcons>.Get().Teleport
                );
            }
        }

    }

}
