using System;
using Battle.State;
using Battle.Weapons.Crosshairs;
using DataTransfer.Data;
using Geometry;
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
        protected virtual void OnLastAttack () { if (!Removable) InitRetreat(3000); }
        protected override void OnUnequip () {}
        protected virtual void OnNumberPress (int n) {}
        protected virtual void OnUpdate () {}


        public override void Update (TurnData td) {
            base.Update(td);
            if (!Equipped) return;

            if (Fires) {
                if (--_shotCooldown <= 0) Shoot();
                return;
            }

            if (--_attackCooldown > 0) return;

            GameTimer.Frozen = false;
            if (td.MB) {
                if (RequiresClick && !_ready) return;
                if (!Removable) LockArsenal();
                GameTimer.Frozen = true;
                if (ConstPower || ++Power >= 50) BeginAttack();
            } else {
                _ready = true;
                // if we released button while holding grenade, throw it
                if (Power > 0) BeginAttack();
            }
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

    }

}
