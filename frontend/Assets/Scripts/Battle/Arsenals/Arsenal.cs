using System;
using Battle.Weapons;


namespace Battle.Arsenals {

    public class Arsenal {
        
        protected int[] Ammo { get; set; }


        public int GetAmmo (int id) {
            return Ammo[id];
        }


        public Weapon GetWeapon (int id) {
            return null; // todo: implement this
        }


        public void UseAmmo (int id, int ammo = 1) {
            if (Ammo[id] < 0) return;
            if (Ammo[id] < ammo) {
                throw new InvalidOperationException("Attempt to use weapon which you don't own");
            }
            Ammo[id] -= ammo;
        }


        public void AddAmmo (int id, int ammo) {
            Ammo[id] += ammo;
        }


        public void SetAmmo (int id, int ammo) {
            Ammo[id] = ammo;
        }


        public void SetAmmo (WeaponId id, int ammo) {
            SetAmmo((int) id, ammo);
        }

    }

}
