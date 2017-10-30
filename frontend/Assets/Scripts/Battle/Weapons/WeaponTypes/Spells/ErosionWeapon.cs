using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Erosion)]
    public class ErosionWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Erosion,
                    The<WeaponIcons>.Get().Erosion
                );
            }
        }

        protected override void OnShoot () {}

    }

}
