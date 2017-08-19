using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class WeaponButton : MonoBehaviour {

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;
        [SerializeField] private Image _image;


        public void OnClick () {
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
        }


        public void SetImage (Sprite sprite) {
            _image.sprite = sprite;
        }


        public void SetAmmo (int ammo) {
            _image.enabled = ammo != 0;
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
