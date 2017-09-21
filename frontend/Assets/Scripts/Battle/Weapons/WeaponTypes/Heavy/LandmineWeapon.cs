﻿using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Landmine)]
    public class LandmineWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Landmine,
                    The<WeaponIcons>.Get().Landmine
                );
            }
        }

        protected override void OnShoot () {}

    }

}