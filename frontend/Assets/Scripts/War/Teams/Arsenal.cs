using System;
using War.Weapons;


namespace War.Teams {

    public class Arsenal {

        public int GetAmmo (int id) {
            return 0;
        }


        public Weapon GetWeapon (int id) {
            return null;
        }


        public void WasteAmmo (int id) {
            if (GetAmmo(id) <= 0) throw new InvalidOperationException("Attempt to use weapon which you don't own");
        }


        public void AddAmmo (int id, int amount) {}


        public void SetAmmo (int id, int amount) {}

    }

}
