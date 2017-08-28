﻿using System;
using Battle.Weapons;
using Utils.Messenger;


namespace Battle.Arsenals {

    public class Arsenal {

        public Messenger<int, int> OnAmmoChanged { get; private set; }

        private int[] Ammo { get; set; }

        
        private int this [int id] {
            get { return Ammo[id]; }
            set {
                Ammo[id] = value;
                OnAmmoChanged.Send(id, value);
            }
        }


        public int GetAmmo (int id) {
            return this[id];
        }


        public Weapon GetWeapon (int id) {
            return null; // todo: implement this
        }


        public void UseAmmo (int id, int ammo = 1) {
            if (this[id] < 0) return; // infinite ammo
            if (this[id] < ammo) {
                throw new InvalidOperationException("Attempt to use weapon which you don't own");
            }
            this[id] -= ammo;
        }


        public void AddAmmo (int id, int ammo) {
            this[id] += ammo;
        }


        public void SetAmmo (int id, int ammo) {
            this[id] = ammo;
        }


        public void SetAmmo (WeaponId id, int ammo) {
            SetAmmo((int) id, ammo);
        }

    }

}