using System;
using Battle.Weapons;
using Utils.Singleton;


namespace Battle.Teams {

    public class Arsenal {

        private BattleScheme _scheme = The<BattleScheme>.Get();
        private int[] Weapons { get; set; }


        public int GetAmmo (int id) {
            return Weapons[id];
        }


        public Weapon GetWeapon (int id) {
            return _scheme.GetWeapon(id);
        }


        public void WasteAmmo (int id, int ammo = 1) {
            if (Weapons[id] <= 0) throw new InvalidOperationException("Attempt to use weapon which you don't own");
            Weapons[id] -= ammo;
        }


        public void AddAmmo (int id, int ammo) {}


        public void SetAmmo (int id, int ammo) {}

    }

}
