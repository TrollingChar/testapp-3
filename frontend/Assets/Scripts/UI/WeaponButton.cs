using UnityEngine;
using UnityEngine.UI;
using War.Weapons;


namespace UI {

    public class WeaponButton : MonoBehaviour {

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;
        private GameObject _image;


        public void Configure (WeaponDescriptor descriptor) {
            _image = Instantiate(descriptor.Icon, transform, false);
            _image.name = "Icon";
            _image.transform.SetAsFirstSibling();

            _id = descriptor.Id;
            
            SetAmmo(1);
        }


        public void OnClick () {
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
            Debug.Log("weapon id: " + _id);
        }


        public void SetAmmo (int ammo) {
            _image.SetActive(ammo != 0);
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
