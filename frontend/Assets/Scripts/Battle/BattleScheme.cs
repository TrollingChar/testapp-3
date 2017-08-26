using System;
using System.Collections.Generic;
using Battle.Weapons;


namespace Battle {

    public class BattleScheme {

        private List<Func<Weapon>> _weapons = new List<Func<Weapon>>();

        protected void AddWeapon<T> () where T : Weapon, new() {
            
        }


        public Weapon GetWeapon (int id) {
            return _weapons[id]();
        }

    }

}
