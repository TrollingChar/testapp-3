using Assets;
using Utils.Singleton;


namespace War.Weapons.Heavy {

    public class DynamiteWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Dynamite,
                    The<WeaponIcons>.Get().Dynamite
                );
            }
        }


    }

}
