using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Heavy {

    public class LandmineWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Landmine,
                    The<WeaponIcons>.Get().Landmine
                );
            }
        }


    }

}
