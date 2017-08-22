using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class WeaponButton : MonoBehaviour {

        public const string IconName = "Icon";

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;

//        [SerializeField] private Image _image;

        private GameObject _image;
        private GameObject Image {
            get { return _image ?? (_image = transform.Find(IconName).gameObject); }
        }


        public void OnClick () {
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
        }


        public void SetAmmo (int ammo) {
            Image.SetActive(ammo != 0);
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
