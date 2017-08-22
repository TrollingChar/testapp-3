using UnityEngine;


namespace War.Weapons {

    public class WeaponDescriptor {
        
        public readonly int Id;
        public readonly GameObject Icon;


        public WeaponDescriptor (int id, GameObject icon) {
            Id = id;
            Icon = icon;
        }

    }  

}
