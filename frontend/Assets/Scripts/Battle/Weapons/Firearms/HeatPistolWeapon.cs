using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Firearms {

    public class HeatPistolWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.HeatPistol,
                    The<WeaponIcons>.Get().HeatPistol
                );
            }
        }

    }

}
