using Battle;
using Battle.State;
using Battle.Teams;
using Battle.Weapons;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;


namespace Core.UI {

    public class WeaponButton : MonoBehaviour {

        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private Text _text;
        private GameObject _image;

        private readonly BattleScene _battleScene = The<BattleScene>.Get();


        public void Configure (WeaponDescriptor descriptor) {
            _image = Instantiate(descriptor.Icon, transform, false);
            _image.name = "Icon";
            _image.transform.SetAsFirstSibling();

            _id = descriptor.Id;

            SetAmmo(1);
        }


        public void OnClick () {
            var teamManager = The<TeamManager>.Get();
            var weaponWrapper = The<WeaponWrapper>.Get();
            // if my turn
            // if arsenal not locked
            // if have that weapon
            // EQUIP
            if (!teamManager.IsMyTurn) return;

            weaponWrapper.PreparedId = _id;
            _battleScene.ArsenalPanel.Hide();
        }


        public void SetAmmo (int ammo) {
            _image.SetActive(ammo != 0);
            _text.text = ammo > 0 ? ammo.ToString() : "";
        }

    }

}
