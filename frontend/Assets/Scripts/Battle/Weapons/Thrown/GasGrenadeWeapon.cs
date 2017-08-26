using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class GasGrenadeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.GasGrenade,
                    The<WeaponIcons>.Get().GasGrenade
                );
            }
        }

        

    }

}
