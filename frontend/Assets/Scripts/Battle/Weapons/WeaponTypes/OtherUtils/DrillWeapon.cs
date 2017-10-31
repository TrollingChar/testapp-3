using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Drill)]
    public class DrillWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Drill,
                    The.WeaponIcons.Drill
                );
            }
        }

    }

}
