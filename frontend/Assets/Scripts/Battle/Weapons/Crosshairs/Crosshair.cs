using UnityEngine;


namespace Battle.Weapons.Crosshairs {

    public abstract class Crosshair {

        protected Weapon Weapon;

        public virtual void OnAdd () {}
        public virtual void OnRemove () {}

    }

}
