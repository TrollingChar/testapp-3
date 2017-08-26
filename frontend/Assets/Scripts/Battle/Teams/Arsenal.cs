using System;
using Battle.Weapons;


namespace Battle.Teams {

    public class Arsenal {

        private int[] Weapons { get; set; }


        public Arsenal () {}


        public int GetAmmo (int id) {
            return Weapons[id];
        }


        public Weapon GetWeapon (int id) {
            return null;
        }


        public void WasteAmmo (int id, int ammo = 1) {
            if (Weapons[id] <= 0) throw new InvalidOperationException("Attempt to use weapon which you don't own");
            Weapons[id] -= ammo;
        }


        public void AddAmmo (int id, int ammo) {}


        public void SetAmmo (int id, int ammo) {}

    }

}
