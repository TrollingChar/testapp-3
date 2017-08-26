using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class PistolWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Pistol,
                    The<WeaponIcons>.Get().Pistol
                );
            }
        }

    }

}
