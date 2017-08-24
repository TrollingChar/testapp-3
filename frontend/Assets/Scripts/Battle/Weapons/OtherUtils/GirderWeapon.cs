using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class GirderWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Girder,
                    The<WeaponIcons>.Get().Girder
                );
            }
        }

    }

}
