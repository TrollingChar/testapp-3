using System;
using System.Collections.Generic;
using Battle.Arsenal;
using Battle.Weapons;


namespace Battle {

    public class BattleScheme {

        private List<Func<Weapon>> _weapons = new List<Func<Weapon>>();


        public Weapon GetWeapon (int id) {
            return _weapons[id]();
        }

    }

}
