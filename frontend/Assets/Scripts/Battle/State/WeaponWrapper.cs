using Battle.Weapons;
using Core;
using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.State {

    public class WeaponWrapper {

        private readonly ActiveWormWrapper _activeWorm = The<ActiveWormWrapper>.Get();
        private bool _locked;
        private Weapon _weapon;


        public WeaponWrapper () {
            The<WeaponWrapper>.Set(this);
        }


        public int PreparedId { get; set; }


        public void Update (TurnData td) {
            if (!_locked && PreparedId != 0) SelectWeapon(PreparedId);
            PreparedId = 0;
            if (_weapon != null) _weapon.Update(td);
        }


        private void SelectWeapon (int weaponId) {
            _weapon = Serialization<Weapon>.GetNewInstanceByCode(weaponId);
            _weapon.Equip(_activeWorm.Worm);
        }


        public void Reset () {
            PreparedId = 0;
            _weapon = null;
            _locked = false;
        }


        public void LockSelect () {
            _locked = true;
        }


        public void Unequip () {
            _weapon = null;
            _locked = true;
        }

    }

}
