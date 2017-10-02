using System;
using Battle.State;
using Battle.Weapons.Crosshairs;
using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.Weapons {

    public abstract class StandardWeapon : Weapon {
        // bug: fires multiple times

        private int _attackCooldown;
        private int _shotCooldown;
        private int _shotsLeft;
        private bool _fires;
        private int _power;
        private bool _ready; // used when weapon requires click

        protected int AttackCooldown = 25;
        protected bool ConstPower = true; // if false, will require to hold the button
        protected int Attacks = 1; // blaster has 2
        protected bool Removable = false; // if false, locks arsenal when used
        protected bool RequiresClick = true; // if true, will fire 2nd shot when button is clicked, if false - when held
        protected int ShotCooldown = 5;
        protected int Shots = 1; // machine gun has many


        protected override void OnEquip () {}
//        protected virtual void OnFirstAttack () {}
        protected virtual void OnBeginAttack () {}
        protected virtual void OnShoot () {}
        protected virtual void OnEndAttack () {}
        protected virtual void OnLastAttack () { InitRetreat(3000); }
        protected override void OnUnequip () {}
        protected virtual void OnNumberPress (int n) {}


        public override void Update (TurnData td) {
            if (!_equipped) return;

            if (_fires) {
                if (--_shotCooldown <= 0) Shoot();
                return;
            }

            if (--_attackCooldown > 0) return;

            GameTimer.Frozen = false;
            if (td.MB) {
                if (RequiresClick && !_ready) return;
                if (!Removable) LockArsenal();
                GameTimer.Frozen = true;
                if (ConstPower || ++_power >= 50) BeginAttack();
            } else {
                _ready = true;
                // if we released button while holding grenade, throw it
                if (_power > 0) BeginAttack();
            }
        }


        private void BeginAttack () {
            GameTimer.Frozen = true;
            _ready = false;
            Attacks--;
            _shotsLeft = Shots;
            _fires = true;
            OnBeginAttack();
            Shoot();
        }


        private void Shoot () {
            _shotsLeft--;
            OnShoot();
            if (_shotsLeft <= 0) {
                EndAttack();
                return;
            }
            _shotCooldown = ShotCooldown;
        }


        private void EndAttack () {
            _fires = false;
            _power = 0;
            OnEndAttack();
            if (Attacks <= 0) {
                OnLastAttack();
                Unequip();
                return;
            }
            _attackCooldown = AttackCooldown;
        }

    }

}
