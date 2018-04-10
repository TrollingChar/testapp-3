using UnityEngine;


namespace Battle.Weapons {

    public class WeaponDescriptor {

        public readonly int Id;
        public readonly GameObject Icon;
        public readonly string Name;


        public WeaponDescriptor (WeaponId id, GameObject icon, string name) {
            Id = (int) id;
            Icon = icon;
            Name = name;
        }

    }

}
