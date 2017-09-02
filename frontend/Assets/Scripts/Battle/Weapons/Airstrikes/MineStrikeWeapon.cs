using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Airstrikes {

    [Weapon(WeaponId.MineStrike)]
    public class MineStrikeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MineStrike,
                    The<WeaponIcons>.Get().MineStrike
                );
            }
        }

    }

}
