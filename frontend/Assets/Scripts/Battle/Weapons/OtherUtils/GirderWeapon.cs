using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class GirderWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Girder,
                    The<WeaponIcons>.Get().Girder
                );
            }
        }

    }

}
