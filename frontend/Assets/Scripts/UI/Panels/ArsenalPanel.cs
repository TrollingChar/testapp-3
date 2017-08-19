using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using War.Weapons;


namespace UI.Panels {

    public class ArsenalPanel : MonoBehaviour {

        [SerializeField] private GameObject _weaponButton;

        private void Start () {
            var i = AddWeapon(BazookaWeapon.Descriptor);
            AddEmpty(3);
            var j = AddWeapon(GrenadeWeapon.Descriptor);
            AddEmpty(3);
        }


        private WeaponButton AddWeapon (WeaponDescriptor descriptor) {
            var button = Instantiate(_weaponButton, gameObject.transform, false);
            var component = button.GetComponent<WeaponButton>();
            
            component.SetImage(descriptor.Icon);
            component.SetAmmo(descriptor.Id);
            
            button.transform.SetAsLastSibling();
            return component;
        }


        private void AddEmpty (int count = 1) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButton, gameObject.transform, false);
                var component = button.GetComponent<WeaponButton>();
                component.SetAmmo(0);
                button.transform.SetAsLastSibling();
            }
        }

    }

}
