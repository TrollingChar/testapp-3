using Assets;
using Utils.Singleton;


namespace War.Weapons.OtherUtils {

    public class SurrenderWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Surrender,
                    The<WeaponIcons>.Get().Surrender
                );
            }
        }

    }

}
