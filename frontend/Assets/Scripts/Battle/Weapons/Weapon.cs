﻿using System.Linq;
using System.Reflection;
using Attributes;
using Battle.Arsenals;
using Battle.Objects;
using Battle.State;
using Core;
using DataTransfer.Data;
using UnityEngine;
using Component = Battle.Objects.Component;
using Time = Core.Time;


namespace Battle.Weapons {

    public abstract class Weapon : Component {

        private readonly int _id;

        private readonly WeaponWrapper _weaponWrapper;
        private Arsenal _arsenal;
        protected bool Equipped;
        protected TimerWrapper GameTimer;

        protected GameObject GameObject;
        public TurnData TurnData { get; private set; }


        protected Weapon () {
            _weaponWrapper = The.WeaponWrapper;
            GameTimer = The.TimerWrapper;
            _id = ((WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute)).Id;
        }


//        public Worm Worm { get; set; }

//        private Crosshair _crossHair;
//        protected Crosshair CrossHair {
//            get { return _crossHair; }
//            set {
//                if (_crossHair != null) {
//                    _crossHair.OnRemove();
//                    //_crossHair.Weapon = null;
//                }
//                if (value != null) {
//                    value.Weapon = this;
//                    value.OnAdd();
//                }
//                _crossHair = value;
//            }
//        }


        public override void OnAdd () {
            _arsenal = ((Worm) Object).Team.Arsenal;
            GameObject = new GameObject(GetType().ToString());
            GameObject.transform.SetParent(Object.GameObject.transform, false);
            Equipped = true;
            OnEquip();
        }


        public override void OnRemove () {
            if (Equipped) Unequip();
        }


        public void Unequip () {
            if (!Equipped) return;
            Equipped = false;
            OnUnequip();
            UnityEngine.Object.Destroy(GameObject);
            ((Worm) Object).Weapon = null;
        }


        protected virtual void OnEquip () {}
        protected virtual void OnUnequip () {}


        public virtual void Update (TurnData td) {
            TurnData = td;
        }


        protected void UseAmmo () {
            _arsenal.UseAmmo(_id);
        }


        protected int GetAmmo () {
            return _arsenal.GetAmmo(_id);
        }


        protected void LockArsenal () {
            _weaponWrapper.Lock();
        }


        public void InitRetreat (Time t) {
           The.BattleScene.InitRetreat(t);
        }


        public static Weapon ById (WeaponId id) {
            return ById((byte) id);
        }


        public static Weapon ById (int id) {
            return ById((byte) id);
        }


        public static Weapon ById (byte id) {
            return Serialization<Weapon>.GetNewInstanceByCode(id);
        }


        public static WeaponDescriptor DescriptorById (WeaponId id) {
            return DescriptorById((byte) id);
        }


        public static WeaponDescriptor DescriptorById (byte id) {
            return (WeaponDescriptor) Serialization<Weapon>.GetTypeByCode(id).GetProperty(
                "Descriptor",
                BindingFlags.Static | BindingFlags.Public
            ).GetValue(null, null);
        }

    }

}
