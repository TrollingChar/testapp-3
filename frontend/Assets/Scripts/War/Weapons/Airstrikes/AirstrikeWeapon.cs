using Assets;
using Utils.Singleton;


namespace War.Weapons.Airstrikes {

    public class AirstrikeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Airstrike,
                    The<WeaponIcons>.Get().Airstrike
                );
            }
        }

    }

}
