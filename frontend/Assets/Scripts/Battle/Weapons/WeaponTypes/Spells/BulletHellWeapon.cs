using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.BulletHell)]
    public class BulletHellWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.BulletHell,
                    The<WeaponIcons>.Get().BulletHell
                );
            }
        }

        protected override void OnShoot () {}

    }

}
