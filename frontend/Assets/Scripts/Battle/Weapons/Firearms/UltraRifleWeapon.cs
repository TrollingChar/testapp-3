using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class UltraRifleWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.UltraRifle,
                    The<WeaponIcons>.Get().UltraRifle
                );
            }
        }

    }

}
