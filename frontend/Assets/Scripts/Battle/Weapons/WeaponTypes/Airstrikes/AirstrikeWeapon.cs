using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.Airstrike)]
    public class AirstrikeWeapon : AbstractAirstrikeWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Airstrike,
                    The.WeaponIcons.Airstrike,
                    "бомбардировка"
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.LaunchAirstrike(
                () => new MultiLauncherShell(),
                Target,
                Direction == PointCrosshair.Direction.Right,
                7
            );
        }

    }

}
