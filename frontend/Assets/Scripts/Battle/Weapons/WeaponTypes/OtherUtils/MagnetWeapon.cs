using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Magnet)]
    public class MagnetWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Magnet,
                    The.WeaponIcons.Magnet
                );
            }
        }

        protected override void OnShoot () {}

    }

}
