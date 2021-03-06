﻿using Attributes;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Controllers;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Jumper)]
    public class JumperWeapon : StandardWeapon {

        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The.WeaponIcons.Jumper,
                    "прыгалка"
                );
            }
        }


        protected override void OnEquip () {
            Removable = true;
            ConstPower = false;

            _crosshair = UnityEngine.Object.Instantiate(
                The.BattleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
            
            ((Worm) Object).NewWormGO.UnlockHead ();
            
            SetCamera ();
        }


        private void SetCamera () {
            The.Camera.Controller = new ObjectBoundCameraController (Object, 0.5f);
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            ((Worm) Object).LookAt (TurnData.XY);
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Controller = new WormJumpCtrl();
            Object.Velocity =
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseJumperSpeed);
        }

    }

}
