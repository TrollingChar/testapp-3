﻿using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Flamethrower)]
    public class FlamethrowerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Flamethrower,
                    The.WeaponIcons.Flamethrower,
                    "огнемет"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
