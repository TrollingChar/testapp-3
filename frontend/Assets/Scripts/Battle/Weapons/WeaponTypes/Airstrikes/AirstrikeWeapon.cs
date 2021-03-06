﻿using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;


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
                () => new Bomb(),
                Target,
                Direction == PointCrosshair.Direction.Right,
                7
            );
        }

    }

}
