using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class WormSelectWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.WormSelect,
                    The<WeaponIcons>.Get().WormSelect
                );
            }
        }

    }

}
