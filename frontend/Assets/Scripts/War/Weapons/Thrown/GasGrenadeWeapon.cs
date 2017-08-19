using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class GasGrenadeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.GasGrenade,
                    The<WeaponIcons>.Get().GasGrenade
                );
            }
        }

        

    }

}
