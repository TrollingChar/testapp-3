using System;
using Battle.Weapons.Crosshairs;


namespace Battle.Weapons {

    public abstract class StandardWeapon : Weapon {

        private bool _equipped;
        private int _attackCooldown;
        private int _attacksLeft; // bug: not initialized
        private int _shotCooldown;
        private int _shotsLeft;
        private bool _fires;
        private int _power;
        private bool _ready; // used when weapon requires click
        protected int AttackCooldown = 25;
        protected int Attacks = 1; // blaster has 2 // bug: no usage
        protected bool ConstPower = true; // if false, will require to hold the button. true for grenade, false for machine gun
        protected bool Removable = false; // if false, locks arsenal when used
        protected bool RequiresClick = true; // if true, will fire 2nd shot when button is clicked, if false - when held
        protected int ShotCooldown = 5;
        protected int Shots = 1; // machine gun has many

        protected Crosshair CrossHair { get; set; }

        // todo: use GameStateController
        public bool TimerFrozen { get; set; }


        protected override void OnEquip() {}
        protected virtual void OnFirstAttack () {} // bug: no usage
        protected virtual void OnBeginAttack () {}
        protected virtual void OnShoot () {}
        protected virtual void OnEndAttack () {}
        protected override void OnUnequip () {}
        protected virtual void OnNumberPress (int n) {}


        public override void Update (TurnData td) {
            if (!_equipped) return;

            if (_fires) {
                if (--_shotCooldown <= 0) Shoot();
                return;
            }

            if (--_attackCooldown > 0) return;

            TimerFrozen = false;
            if (td.MB) {
                if (RequiresClick && !_ready) return;
                if (!Removable) LockArsenal();
                TimerFrozen = true;
                if (ConstPower || ++_power >= 50) BeginAttack();
            } else {
                _ready = true;
                // if we released button while holding grenade, throw it
                if (_power > 0) BeginAttack();
            }
        }


        private void BeginAttack () {
            TimerFrozen = true;
            _ready = false;
            _attacksLeft--;
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
            OnEndAttack();
            _fires = false;
            _power = 0;
            if (_attacksLeft <= 0) {
                Unequip();
                return;
            }
            _attackCooldown = AttackCooldown;
        }


        private void Unequip () {
            if (!_equipped) return;
            _equipped = false;
            OnUnequip();
        }

    }
}
