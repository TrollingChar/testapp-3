using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.MineStrike)]
    public class MineStrikeWeapon : AbstractAirstrikeWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MineStrike,
                    The.WeaponIcons.MineStrike,
                    "сброс мин"
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.LaunchAirstrike(
                () => new Landmine(),
                Target,
                Direction == PointCrosshair.Direction.Right,
                5
            );
        }

    }

}
