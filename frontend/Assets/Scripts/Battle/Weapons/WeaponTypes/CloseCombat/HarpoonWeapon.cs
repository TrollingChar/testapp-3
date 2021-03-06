﻿using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Harpoon)]
    public class HarpoonWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Harpoon,
                    The.WeaponIcons.Harpoon,
                    "гарпун"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
