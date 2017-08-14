using System;
using War.Weapons;


namespace War {

    public class WeaponFactory {

        private readonly Func<Weapon>[] _weapons;


        // todo: accept weapon descriptors
        // - name
        // - description
        // - function () : weapon
        public WeaponFactory (params Func<Weapon>[] weapons) {
            _weapons = new Func<Weapon>[weapons.Length + 1];
            Array.Copy(weapons, 0, _weapons, 1, weapons.Length);
        }


        public Weapon this [int index] {
            get {
                var weapon = _weapons[index]();
                weapon.Id = index;
                return weapon;
            }
        }

    }

}
