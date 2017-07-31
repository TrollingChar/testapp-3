using System;


namespace War.Weapons {

    public static class Weapons {

        private static Func<Weapon>[] _weaponFactories = {
            // do not remove them from middle of array, replace with null or last element
            null, // we did not select weapon, or arsenal button must be empty 
            () => new WBazooka(),
            () => new WGrenade(),
        };

    }

}
