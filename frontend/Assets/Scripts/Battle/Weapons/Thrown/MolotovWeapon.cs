using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class MolotovWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.Molotov,
                    The<WeaponIcons>.Get().Molotov
                );
            }
        }

        

    }

}
