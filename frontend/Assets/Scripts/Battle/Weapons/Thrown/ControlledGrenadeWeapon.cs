using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class ControlledGrenadeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ControlledGrenade,
                    The<WeaponIcons>.Get().ControlledGrenade
                );
            }
        }

        

    }

}
