using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class ParachuteWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Parachute,
                    The<WeaponIcons>.Get().Parachute
                );
            }
        }

    }

}
