using System;
using System.Linq;
using Attributes;
using Battle.Arsenals;
using Battle.Objects;
using Battle.State;
using Battle.Weapons.Crosshairs;
using DataTransfer.Data;
using UnityEngine;
using Utils.Singleton;
using Component = Battle.Objects.Component;


namespace Battle.Weapons {

    public abstract class Weapon : Component {

        private readonly int _id;

        private readonly WeaponWrapper _weaponWrapper;
        private Arsenal _arsenal;
        protected bool _equipped;
        protected TimerWrapper GameTimer;

        protected GameObject GameObject;


        protected Weapon () {
            _weaponWrapper = The<WeaponWrapper>.Get();
            GameTimer = The<TimerWrapper>.Get();
            _id = ((WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute)).Id;
        }


//        public Worm Worm { get; set; }

        private Crosshair _crossHair;
        protected Crosshair CrossHair {
            get { return _crossHair; }
            set {
                if (_crossHair != null) {
                    _crossHair.OnRemove();
                    //_crossHair.Weapon = null;
                }
                if (value != null) {
                    value.Weapon = this;
                    value.OnAdd();
                }
                _crossHair = value;
            }
        }


        [Obsolete]
        public void Equip (Worm worm) {
//            Worm = worm;
            _arsenal = worm.Team.Arsenal;
            _equipped = true;
            OnEquip();
        }


        public override void OnAdd () {
            _arsenal = ((Worm) Object).Team.Arsenal;
            GameObject = new GameObject(GetType().ToString());
            GameObject.transform.SetParent(Object.GameObject.transform, false);
            _equipped = true;
            OnEquip();
        }


        public override void OnRemove () {
            UnityEngine.Object.Destroy(GameObject);
        }


        public void Unequip () {
            if (!_equipped) return;
            _equipped = false;
            OnUnequip();
            ((Worm) Object).Weapon = null;
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


        protected void InitRetreat (int milliseconds) {
            _weaponWrapper.LockAndUnequip();
            GameTimer.Time = milliseconds;
            GameTimer.Frozen = false;
        }

    }

}
