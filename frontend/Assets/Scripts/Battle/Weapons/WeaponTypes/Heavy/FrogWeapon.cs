﻿using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Frog)]
    public class FrogWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Frog,
                    The.WeaponIcons.Frog,
                    "лягушка"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
