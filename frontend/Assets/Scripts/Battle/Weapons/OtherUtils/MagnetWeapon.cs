using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class MagnetWeapon {

        
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
