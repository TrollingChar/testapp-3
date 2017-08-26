using Assets;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    public class SkipTurnWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.SkipTurn,
                    The<WeaponIcons>.Get().SkipTurn
                );
            }
        }

    }

}
