using Attributes;
using Battle.Objects;
using Battle.Objects.Timers;
using Core;
using Geometry;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.BulletHell)]
    public class BulletHellWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.BulletHell,
                    The.WeaponIcons.BulletHell,
                    "свинцовый дождь"
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            var effector = new Effector();
            The.World.Spawn(effector, XY.Zero);
            effector.Timer = new BulletHellTimer();
        }

    }

}
