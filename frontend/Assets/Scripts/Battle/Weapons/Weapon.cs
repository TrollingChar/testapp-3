using System.Linq;
using System.Reflection;
using Attributes;
using Battle.Arsenals;
using Battle.Experimental;
using Battle.Objects;
using Core;
using DataTransfer.Data;
using UnityEngine;
using Component = Battle.Objects.Component;
using Time = Core.Time;


namespace Battle.Weapons {

    public abstract class Weapon : Component {

        private readonly int _id;

        private          Arsenal       _arsenal;
        protected        bool          Equipped;

        protected GameObject GameObject;
        public    TurnData   TurnData { get; private set; }


        protected Weapon () {
            _id = ((WeaponAttribute) GetType ().GetCustomAttributes (true).First (a => a is WeaponAttribute)).Id;
        }


        public override void OnAdd () {
            _arsenal   = ((Worm) Object).Team.Arsenal;
            GameObject = new GameObject (GetType ().ToString ());
            GameObject.transform.SetParent (Object.GameObject.transform, false);
            
            Equipped = true;
            OnEquip ();
        }


        public override void OnRemove () {
            if (Equipped) Unequip ();
        }


        public void Unequip () {
            if (!Equipped) return;
            Equipped = false;
            OnUnequip ();
            UnityEngine.Object.Destroy (GameObject);
            ((Worm) Object).Weapon = null;
        }


        protected virtual void OnEquip ()   {}
        protected virtual void OnUnequip () {}


        public virtual void Update (TurnData td) {
            TurnData = td;
        }


        protected void UseAmmo () {
            _arsenal.UseAmmo (_id);
        }


        protected int GetAmmo () {
            return _arsenal.GetAmmo (_id);
        }


        protected void LockArsenal () {
//            _weaponWrapper.Lock ();
            The.Battle.LockArsenal ();
        }


        public void InitRetreat (Time t) {
            The.Battle.InitRetreat (t);
        }


        public static Weapon ById (WeaponId id) {
            return ById ((byte) id);
        }


        public static Weapon ById (int id) {
            return ById ((byte) id);
        }


        public static Weapon ById (byte id) {
            return Serialization <Weapon>.GetNewInstanceByCode (id);
        }


        public static WeaponDescriptor DescriptorById (WeaponId id) {
            return DescriptorById ((byte) id);
        }


        public static WeaponDescriptor DescriptorById (byte id) {
            return (WeaponDescriptor) Serialization <Weapon> .GetTypeByCode (id).GetProperty (
                "Descriptor",
                BindingFlags.Static | BindingFlags.Public
            ).GetValue (null, null);
        }

    }

}