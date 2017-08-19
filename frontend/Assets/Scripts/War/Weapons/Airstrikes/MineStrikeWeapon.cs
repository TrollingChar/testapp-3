using Assets;
using Utils.Singleton;


namespace War.Weapons.Airstrikes {

    public class MineStrikeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.MineStrike,
                    The<WeaponIcons>.Get().MineStrike
                );
            }
        }

    }

}
