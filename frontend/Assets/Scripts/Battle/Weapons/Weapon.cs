using System.Linq;
using Attributes;
using Battle.Arsenals;
using Battle.Objects;
using Battle.State;
using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.Weapons {

    public abstract class Weapon {

        private readonly int _id;

        private readonly WeaponWrapper _weaponWrapper;
        private Arsenal _arsenal;
        protected bool _equipped;
        protected TimerWrapper GameTimer;


        protected Weapon () {
            _weaponWrapper = The<WeaponWrapper>.Get();
            GameTimer = The<TimerWrapper>.Get();
            _id = ((WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute)).Id;
        }


        public Worm Worm { get; set; }


        public void Equip (Worm worm) {
            Worm = worm;
            _arsenal = worm.Team.Arsenal;
            _equipped = true;
            OnEquip();
        }


        public void Unequip () {
            if (_equipped) return;
            _equipped = false;
            OnUnequip();
        }


        protected virtual void OnEquip () {}
        protected virtual void OnUnequip () {}
        public abstract void Update (TurnData td);


        protected void UseAmmo () {
            _arsenal.UseAmmo(_id);
        }


        protected int GetAmmo () {
            return _arsenal.GetAmmo(_id);
        }


        protected void LockArsenal () {
            _weaponWrapper.Lock();
        }


        protected void InitRetreat(int milliseconds)
        {
            _weaponWrapper.LockAndUnequip();
            GameTimer.Time = milliseconds;
            GameTimer.Frozen = false;
        }

    }

}
