using UnityEngine;
using UnityEngine.UI;
using War.Weapons;


namespace UI.Panels {

    public class ArsenalPanel : MonoBehaviour {

        [SerializeField] private GameObject _weaponButton;

        private void Start () {
            AddWeapon<BazookaWeapon>();
            AddEmpty(11);
        }


        private void AddWeapon<T> () where T : Weapon {
            var button = Instantiate(_weaponButton);
            var component = _weaponButton.GetComponent<WeaponButton>();
            component.SetAmmo(1);
            button.transform.SetParent(gameObject.transform);
            button.transform.SetAsLastSibling();
//            button.transform.SetAsFirstSibling();
        }


        private void AddEmpty (int count) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButton);
                var component = _weaponButton.GetComponent<WeaponButton>();
                component.SetAmmo(0);
                button.transform.SetParent(gameObject.transform);
                button.transform.SetAsLastSibling();
//                button.transform.SetAsFirstSibling();
            }
        }

    }

}
