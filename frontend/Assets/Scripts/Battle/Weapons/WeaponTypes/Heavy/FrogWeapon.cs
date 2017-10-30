using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Frog)]
    public class FrogWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Frog,
                    The<WeaponIcons>.Get().Frog
                );
            }
        }

        protected override void OnShoot () {}

    }

}
