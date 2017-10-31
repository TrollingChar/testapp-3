using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Cryogun)]
    public class CryogunWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Cryogun,
                    The.WeaponIcons.Cryogun
                );
            }
        }

    }

}
