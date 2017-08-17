using UnityEngine;
using UnityEngine.UI;
using War.Weapons;


namespace UI.Panels {

    public class ArsenalPanel : MonoBehaviour {

        [SerializeField] private GameObject _weaponButton;
        private int _i = 0;

        private void Start () {
            AddWeapon<BazookaWeapon>();
            AddWeapon<GrenadeWeapon>();
            AddEmpty(6);
        }


        private void AddWeapon<T> () where T : Weapon {
            var button = Instantiate(_weaponButton);
            var component = _weaponButton.GetComponent<WeaponButton>();
            component.SetAmmo(1);
            button.transform.SetParent(gameObject.transform, false);
            button.name = "button " + _i++;
            button.transform.SetAsLastSibling();
//            button.transform.SetAsFirstSibling();
        }


        private void AddEmpty (int count = 1) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButton);
                var component = _weaponButton.GetComponent<WeaponButton>();
                component.SetAmmo(0);
                button.transform.SetParent(gameObject.transform, false);
                button.name = "button " + _i++;
                button.transform.SetAsLastSibling();
//                button.transform.SetAsFirstSibling();
            }
        }

    }

}
