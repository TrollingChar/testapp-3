﻿using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Erosion)]
    public class ErosionWeapon : StandardWeapon {

        private GameObject _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Erosion,
                    The.WeaponIcons.Erosion,
                    "эрозия"
                );
            }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate(
                The.BattleAssets.PointCrosshair,
                GameObject.transform,
                false
            );

            // todo: magic scroll or something
//            _sprite = UnityEngine.Object.Instantiate(
//                The<BattleAssets>.Get().GrenadeWeapon,
//                GameObject.transform,
//                false
//            );
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.DestroyTerrain(TurnData.XY, 50f);
        }

    }

}
