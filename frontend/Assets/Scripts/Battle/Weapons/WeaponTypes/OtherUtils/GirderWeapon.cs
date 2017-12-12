using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Girder)]
    public class GirderWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Girder,
                    The.WeaponIcons.Girder
                );
            }
        }

        protected override void OnShoot () {}

    }

}
