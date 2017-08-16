using UnityEngine;
using War.Weapons;


namespace UI.Panels {

    public class ArsenalPanel : MonoBehaviour {

        private void Start () {
            AddWeapon<BazookaWeapon>();
            AddEmpty(62);
        }


        private void AddWeapon<T> () where T : Weapon {}


        private void AddEmpty (int count) {}

    }

}
