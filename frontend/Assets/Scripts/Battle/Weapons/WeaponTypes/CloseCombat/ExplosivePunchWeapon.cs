﻿using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.ExplosivePunch)]
    public class ExplosivePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ExplosivePunch,
                    The.WeaponIcons.ExplosivePunch,
                    "взрывной удар"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
