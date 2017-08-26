using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class RopeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.Rope,
                    The<WeaponIcons>.Get().Rope
                );
            }
        }

    }

}
