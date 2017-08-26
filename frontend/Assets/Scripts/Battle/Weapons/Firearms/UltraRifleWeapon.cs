using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class UltraRifleWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.UltraRifle,
                    The<WeaponIcons>.Get().UltraRifle
                );
            }
        }

    }

}
