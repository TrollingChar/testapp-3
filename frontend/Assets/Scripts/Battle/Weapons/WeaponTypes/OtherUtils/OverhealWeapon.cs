using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Overheal)]
    public class OverhealWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Overheal,
                    The.WeaponIcons.Overheal,
                    "общее исцеление"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
