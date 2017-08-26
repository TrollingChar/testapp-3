using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Airstrikes {

    public class AirstrikeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Airstrike,
                    The<WeaponIcons>.Get().Airstrike
                );
            }
        }

    }

}
