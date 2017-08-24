using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class WormSelectWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.WormSelect,
                    The<WeaponIcons>.Get().WormSelect
                );
            }
        }

    }

}
