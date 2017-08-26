using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class SurrenderWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Surrender,
                    The<WeaponIcons>.Get().Surrender
                );
            }
        }

    }

}
