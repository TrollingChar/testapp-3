using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class HolyGrenadeWeapon {

        

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.HolyGrenade,
                    The<WeaponIcons>.Get().HolyGrenade
                );
            }
        }

    }

}
