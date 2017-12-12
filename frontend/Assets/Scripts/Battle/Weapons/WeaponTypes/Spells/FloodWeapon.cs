using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Flood)]
    public class FloodWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Flood,
                    The.WeaponIcons.Flood
                );
            }
        }

        protected override void OnShoot () {}

    }

}
