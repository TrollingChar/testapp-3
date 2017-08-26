using UnityEngine;


namespace Battle.Weapons {

    public class WeaponDescriptor {
        
        public readonly int Id;
        public readonly GameObject Icon;


        public WeaponDescriptor (WeaponId id, GameObject icon) {
            Id = (int) id;
            Icon = icon;
        }

    }

}
