using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Heavy {

    public class DynamiteWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.Dynamite,
                    The<WeaponIcons>.Get().Dynamite
                );
            }
        }


    }

}
