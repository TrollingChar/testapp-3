using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Overheal)]
    public class OverhealWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Overheal,
                    The.WeaponIcons.Overheal
                );
            }
        }

        protected override void OnShoot () {}

    }

}
