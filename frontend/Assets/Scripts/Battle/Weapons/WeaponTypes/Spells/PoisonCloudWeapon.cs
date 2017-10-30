using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.PoisonCloud)]
    public class PoisonCloudWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonCloud,
                    The<WeaponIcons>.Get().PoisonCloud
                );
            }
        }

        protected override void OnShoot () {}

    }

}
