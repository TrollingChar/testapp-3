using Assets;
using Utils.Singleton;


namespace War.Weapons.OtherUtils {

    public class SkipTurnWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.SkipTurn,
                    The<WeaponIcons>.Get().SkipTurn
                );
            }
        }

    }

}
