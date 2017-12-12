using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Jetpack)]
    public class JetpackWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jetpack,
                    The.WeaponIcons.Jetpack
                );
            }
        }

    }

}
