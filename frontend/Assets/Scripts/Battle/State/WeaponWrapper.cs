using Battle.Weapons;
using Core;
using DataTransfer.Data;
using Utils.Singleton;


namespace Battle.State {

    public class WeaponWrapper {

        private readonly ActiveWormWrapper _activeWorm = The<ActiveWormWrapper>.Get();
        private bool _locked;
        private Weapon _weapon; // todo: should use property?


        public WeaponWrapper () {
            The<WeaponWrapper>.Set(this);
        }


        public int PreparedId { get; set; }


        public void Update (TurnData td) {
            if (!_locked && PreparedId != 0) Select(PreparedId);
            PreparedId = 0;
            if (_weapon != null) _weapon.Update(td);
        }


        private void Select (int weaponId) {
            _activeWorm.Worm.Weapon = _weapon = Serialization<Weapon>.GetNewInstanceByCode(weaponId);
//            _weapon.Equip(_activeWorm.Worm);
        }


        public void Reset () {
            PreparedId = 0;
            _weapon = null;
            _locked = false;
        }


        public void Lock () {
            _locked = true;
        }


        public void LockAndUnequip () {
            _weapon = null;
            _locked = true;
        }

    }

}
