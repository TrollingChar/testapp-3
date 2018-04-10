using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.SkipTurn)]
    public class SkipTurnWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.SkipTurn,
                    The.WeaponIcons.SkipTurn,
                    "пропуск хода"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
