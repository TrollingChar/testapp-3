using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class ControlledGrenadeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.ControlledGrenade,
                    The<WeaponIcons>.Get().ControlledGrenade
                );
            }
        }

        

    }

}
