using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Surrender)]
    public class SurrenderWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Surrender,
                    The.WeaponIcons.Surrender
                );
            }
        }

        protected override void OnShoot () {}

    }

}
