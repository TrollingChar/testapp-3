﻿using Attributes;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.GasGrenade)]
    public class GasGrenadeWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private int _timer = 5;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.GasGrenade,
                    The.WeaponIcons.GasGrenade,
                    "газовая граната"
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.GrenadeWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object)._newWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object)._newWormGO.LockHead ();
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Spawn(
                new GasGrenade(),
                Object.Position,
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseShotSpeed)
            );
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
