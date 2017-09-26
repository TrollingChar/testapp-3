using UnityEngine;


namespace Battle.Weapons {

    public class WeaponDescriptor {

        public readonly GameObject Icon;

        public readonly int Id;


        public WeaponDescriptor (WeaponId id, GameObject icon) {
            Id = (int) id;
            Icon = icon;
        }

    }

}
