using UnityEngine;
using Utils.Singleton;


namespace Assets {

    public class WeaponIcons : MonoBehaviour {

        public GameObject
            Bazooka,
            Grenade;


        private void Awake () {
            The<WeaponIcons>.Set(this);
        }

    }

}
