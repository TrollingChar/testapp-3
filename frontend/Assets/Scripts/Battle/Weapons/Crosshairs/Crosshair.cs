using UnityEngine;


namespace Battle.Weapons.Crosshairs {

    public abstract class Crosshair {

        public Weapon Weapon;

        public virtual void OnAdd () {}
        public virtual void OnRemove () {}

    }

}
