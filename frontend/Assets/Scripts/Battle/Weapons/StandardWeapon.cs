using System;


namespace Battle.Weapons {

    public abstract class StandardWeapon : Weapon {

        private int _attackCooldown;

        private int _attacksLeft;
        private bool _equipped;
        private bool _fires;
        private int _power;
        private bool _ready; // used when weapon requires click
        private int _shotCooldown;
        private int _shotsLeft;
        protected int AttackCooldown;
        protected int Attacks; // blaster has 2
        protected bool ConstPower;
        protected int Power;
        protected bool Removable; // if false, locks arsenal when used
        protected bool RequiresClick; // if true, will fire 2nd shot when button is clicked, if false - when held
        protected int ShotCooldown;
        protected int Shots; // machine gun has many


        public bool TimerFrozen { get; set; }


        protected virtual void OnEquip () {}
        protected virtual void OnFirstAttack () {}
        protected virtual void OnBeginAttack () {}
        protected virtual void OnShoot () {}
        protected virtual void OnEndAttack () {}
        protected virtual void OnUnequip () {}
        protected virtual void OnNumberPress (int n) {}


        public void Equip () {
            Configure();
            _equipped = true;
            OnEquip();
        }


        protected virtual void Configure () {}


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


        private void LockArsenal () {
            throw new NotImplementedException();
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
