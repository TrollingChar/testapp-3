using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Earthquake)]
    public class EarthquakeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Earthquake,
                    The<WeaponIcons>.Get().Earthquake
                );
            }
        }

        protected override void OnShoot () {}

    }

}
