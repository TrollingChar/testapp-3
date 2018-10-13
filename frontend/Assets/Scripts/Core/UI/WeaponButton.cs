using Battle;
using Battle.Experimental;
using Battle.Weapons;
using UnityEngine;
using UnityEngine.UI;


namespace Core.UI {

    public class WeaponButton : MonoBehaviour {

        [SerializeField] private int _weaponId;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;
        private GameObject _image;
        private Button _button;

        private readonly NewBattleScene _battle = The.Battle;

        public int WeaponId {
            get { return _weaponId; }
            private set { _weaponId = value; }
        }


        private void Awake () {
            _button = GetComponent<Button>();
        }


        public void Configure (WeaponDescriptor descriptor) {
            _image = Instantiate(descriptor.Icon, transform, false);
            _image.name = "Icon";
            _image.transform.SetAsFirstSibling();

            WeaponId = descriptor.Id;

            SetAmmo(1);
        }


        public void OnClick () {
//            var teamManager = The.TeamManager;
//            var weaponWrapper = The.WeaponWrapper;
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
//            if (!teamManager.IsMyTurn) return;

            _battle.PrepareWeapon((byte) WeaponId);
            _battle.ArsenalPanel.Hide();
        }


        public void SetAmmo (int ammo) {
            _image.SetActive(ammo != 0);
            _button.interactable = ammo != 0;
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
