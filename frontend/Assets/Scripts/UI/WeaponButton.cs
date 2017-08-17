using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class WeaponButton : MonoBehaviour {

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;


        private void Awake () {
            Debug.Log(_image);
            Debug.Log(_text);
        }


        public void OnClick () {
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
        }


        public void SetImage (GameObject image) {
            
        }


        public void SetAmmo (int ammo) {
            if (ammo == 0) _image.enabled = false;
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
