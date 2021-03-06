﻿using System;
using Attributes;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.MultiLauncher)]
    public class MultiLauncherWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MultiLauncher,
                    The.WeaponIcons.MultiLauncher,
                    "мини-ракеты"
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;
            Shots = Math.Min(5, GetAmmo());
            ShotCooldown.Seconds = 0.2f;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.MultiLauncherWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object).NewWormGO.UnlockHead ();
            
            SetCamera ();
        }


        private void SetCamera () {
            The.Camera.Controller = new ObjectBoundCameraController (Object, 0.5f);
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnNumberPress (int n) {
            Shots = Math.Min(n, GetAmmo());
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.Spawn(
                new MultiLauncherShell(),
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
