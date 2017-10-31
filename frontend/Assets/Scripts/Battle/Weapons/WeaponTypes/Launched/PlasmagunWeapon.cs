using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Plasmagun)]
    public class PlasmagunWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Plasmagun,
                    The.WeaponIcons.Plasmagun
                );
            }
        }

    }

}
