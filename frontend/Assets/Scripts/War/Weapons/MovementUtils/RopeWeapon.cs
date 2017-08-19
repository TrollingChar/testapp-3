using Assets;
using Utils.Singleton;


namespace War.Weapons.MovementUtils {

    public class RopeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Rope,
                    The<WeaponIcons>.Get().Rope
                );
            }
        }

    }

}
