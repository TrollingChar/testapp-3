﻿using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.ControlledGrenade)]
    public class ControlledGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ControlledGrenade,
                    The.WeaponIcons.ControlledGrenade,
                    "граната с дистанционным взрывателем"
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new LineCrosshair();
        }


        protected override void OnShoot () {}

    }

}
