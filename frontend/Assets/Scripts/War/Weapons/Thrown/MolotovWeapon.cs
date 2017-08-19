using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class MolotovWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Molotov,
                    The<WeaponIcons>.Get().Molotov
                );
            }
        }

        

    }

}
