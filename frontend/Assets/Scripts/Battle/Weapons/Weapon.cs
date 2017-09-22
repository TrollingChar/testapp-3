using System;
using System.Linq;
using Attributes;
using Battle.Arsenals;
using Battle.Objects;
using Battle.State;
using Utils.Singleton;


namespace Battle.Weapons {

    public abstract class Weapon {

        private readonly WeaponWrapper _weaponWrapper;
        private readonly int _id;
        protected bool _equipped;
        private Arsenal _arsenal;

        public Worm Worm { get; set; }


        protected Weapon () {
            _weaponWrapper = The<WeaponWrapper>.Get();
            _id = ((WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute)).Id;
        }


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
            _weaponWrapper.LockSelect();
        }

    }

}
