using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Minegun)]
    public class MinegunWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Minegun,
                    The.WeaponIcons.Minegun
                );
            }
        }

    }

}
