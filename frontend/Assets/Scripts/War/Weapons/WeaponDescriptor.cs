using UnityEngine;


namespace War.Weapons {

    public class WeaponDescriptor {

        public readonly int Id;
        public readonly Sprite Icon;


        public WeaponDescriptor (int id, Sprite icon) {
            Id = id;
            Icon = icon;
        }

    }

}
