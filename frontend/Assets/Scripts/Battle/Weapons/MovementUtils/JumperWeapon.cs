using Assets;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    public class JumperWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The<WeaponIcons>.Get().Jumper
                );
            }
        }

    }

}
