using Assets;
using Utils.Singleton;


namespace War.Weapons.Firearms {

    public class PistolWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Pistol,
                    The<WeaponIcons>.Get().Pistol
                );
            }
        }

    }

}
