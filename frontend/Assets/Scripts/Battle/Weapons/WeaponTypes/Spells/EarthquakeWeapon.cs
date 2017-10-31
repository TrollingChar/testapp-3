using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Earthquake)]
    public class EarthquakeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Earthquake,
                    The.WeaponIcons.Earthquake
                );
            }
        }

        protected override void OnShoot () {}

    }

}
