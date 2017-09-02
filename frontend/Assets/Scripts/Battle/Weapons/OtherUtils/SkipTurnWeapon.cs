using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    [Weapon(WeaponId.SkipTurn)]
    public class SkipTurnWeapon : StandardWeapon {

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
