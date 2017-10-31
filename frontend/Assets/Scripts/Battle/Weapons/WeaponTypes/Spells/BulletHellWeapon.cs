using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.BulletHell)]
    public class BulletHellWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.BulletHell,
                    The.WeaponIcons.BulletHell
                );
            }
        }

        protected override void OnShoot () {}

    }

}
