﻿using Attributes;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.HomingMissile)]
    public class HomingMissileWeapon : StandardWeapon {

        private GameObject _pointCH;
        private LineCrosshair _lineCH;
        private GameObject _sprite;

        private XY _target;


        private void ResetTarget () {
            _target = XY.NaN;
            Attacks = 1;
            AttackCooldown.Ticks = 0;
            ConstPower = true;
        }


        private void SetTarget (XY target) {
            _target = target;
            Attacks = 1;
            ConstPower = false;
        }
        

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HomingMissile,
                    The.WeaponIcons.HomingMissile,
                    "самонаводящаяся ракета"
                );
            }
        }


        protected override void OnEquip () {
            var battleAssets = The.BattleAssets;

            _lineCH = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
            
            _pointCH = UnityEngine.Object.Instantiate(battleAssets.PointCrosshair);

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.HomingMissileWeapon,
                GameObject.transform,
                false
            );
            
            ResetTarget();
            ((Worm) Object).NewWormGO.UnlockHead ();
            
            SetCamera ();
        }


        private void SetCamera () {
            The.Camera.Controller = new ObjectBoundCameraController (Object, 0.5f);
        }


        protected override void OnShoot () {
            if (_target.IsNaN) {
                // target not set
                SetTarget(TurnData.XY);
            }
            else {
                UseAmmo();
                The.World.Spawn(
                    new HomingMissile(_target),
                    Object.Position,
                    (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseShotSpeed)
                );
            }
        }


        protected override void OnUpdate () {
            if (_target.IsNaN) { //UpdatePointCrosshair(_pointCH);
                _pointCH.transform.localPosition = new Vector3(TurnData.XY.X, TurnData.XY.Y);
            }
            UpdateLineCrosshair(_lineCH);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }


        protected override void OnUnequip () {
            // прицел не входит в его gameobject, удалять отдельно
            UnityEngine.Object.Destroy(_pointCH);
            ((Worm) Object).NewWormGO.LockHead ();
        }

    }

}
