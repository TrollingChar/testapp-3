using Assets;
using Utils.Singleton;


namespace War.Weapons.MovementUtils {

    public class ParachuteWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Parachute,
                    The<WeaponIcons>.Get().Parachute
                );
            }
        }

    }

}
