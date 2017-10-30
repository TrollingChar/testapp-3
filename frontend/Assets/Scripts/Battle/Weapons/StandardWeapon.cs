﻿using System;
using Battle.State;
using Battle.Weapons.Crosshairs;
using DataTransfer.Data;
using Geometry;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons {

    public abstract class StandardWeapon : Weapon {

        private int _attackCooldown;
        private int _shotCooldown;
        protected int ShotsLeft { get; private set; }
        protected bool Fires { get; private set; }
        protected int Power { get; private set; }
        private bool _ready; // used when weapon requires click

        protected int AttackCooldown = 25;
        protected int ShotCooldown = 5;
        protected bool ConstPower = true; // if false, will require to hold the button
        protected int Attacks = 1; // blaster has 2
        protected bool Removable = false; // if false, locks arsenal when used
        protected bool RequiresClick = true; // if true, will fire 2nd shot when button is clicked, if false - when held
        protected int Shots = 1; // machine gun has many


        protected override void OnEquip () {}


//        protected virtual void OnFirstAttack () {}
        protected virtual void OnBeginAttack () {}


        protected virtual void OnShoot () {}
        protected virtual void OnEndAttack () {}


        protected virtual void OnLastAttack () {
            if (!Removable) InitRetreat(3000);
        }


        protected override void OnUnequip () {}
        protected virtual void OnNumberPress (int n) {}
        protected virtual void OnUpdate () {}


        public override void Update (TurnData td) {
            base.Update(td);
            if (!Equipped) return;

            if (td.NumKey > 0) OnNumberPress(td.NumKey);

            if (Fires) {
                if (--_shotCooldown <= 0) Shoot();
                goto end;
            }

            if (--_attackCooldown > 0) goto end;

            GameTimer.Frozen = false;
            if (td.MB) {
                if (RequiresClick && !_ready) goto end;
                if (!Removable) LockArsenal();
                GameTimer.Frozen = true;
                if (ConstPower || ++Power >= 50) BeginAttack();
            } else {
                _ready = true;
                // if we released button while holding grenade, throw it
                if (Power > 0) BeginAttack();
            }
            end:
            OnUpdate();
        }


        private void BeginAttack () {
            GameTimer.Frozen = true;
            _ready = false;
            Attacks--;
            ShotsLeft = Shots;
            Fires = true;
            OnBeginAttack();
            Shoot();
        }


        private void Shoot () {
            ShotsLeft--;
            OnShoot();
            if (ShotsLeft <= 0) {
                EndAttack();
                return;
            }
            _shotCooldown = ShotCooldown;
        }


        private void EndAttack () {
            Fires = false;
            Power = 0;
            OnEndAttack();
            if (Attacks <= 0) {
                OnLastAttack();
                Unequip();
                return;
            }
            _attackCooldown = AttackCooldown;
        }


        protected void UpdateLineCrosshair (LineCrosshair crosshair) {
            var xy = TurnData.XY - Object.Position;
            if (xy == XY.Zero) xy = XY.Up;
            crosshair.Angle = xy.Angle;
            crosshair.RingVisible = Power > 0;
            crosshair.RingPosition = Power / 50f;
        }


        protected void UpdateAimedWeapon (GameObject sprite) {
            var xy = TurnData.XY - Object.Position;
            if (xy == XY.Zero) xy = XY.Up;
            sprite.transform.localRotation = Quaternion.Euler(0, 0, xy.Angle * Mathf.Rad2Deg);

            float angle = xy.Angle * Mathf.Rad2Deg;
            bool deltaTooBig = Mathf.Abs(Mathf.DeltaAngle(0, angle)) > 90;
            var scale = sprite.transform.localScale;
            scale.x *= scale.x > 0 ^ deltaTooBig ? 1f : -1f;
            sprite.transform.localScale = scale;
            sprite.transform.localEulerAngles = new Vector3(0, 0, angle + (deltaTooBig ? 180 : 0));
        }

    }

}
