using Battle.Weapons;
using Core;
using DataTransfer.Data;
using UnityEngine;


namespace Battle.State {

    public class WeaponWrapper {

        private readonly ActiveWorm _activeWorm = The.ActiveWorm;
        private bool _locked;
        private Weapon _weapon; // todo: should use property?


        public WeaponWrapper () {
            The.WeaponWrapper = this;
        }


//        public int PreparedId { get; set; }


        public void Update (TurnData td) {
            if (!_locked && td != null && td.Weapon != 0) Select(td.Weapon);
            if (_weapon != null) _weapon.Update(td);
        }


        public void Select (int weaponId) {
            _activeWorm.Worm.Weapon = _weapon = Weapon.ById(weaponId);// Serialization<Weapon>.GetNewInstanceByCode(weaponId);
//            _weapon.Equip(_activeWorm.Worm);
        }


        public void Unlock () {
            TurnData.PrepareNoWeapon();
//            PreparedId = 0;
            _activeWorm.Worm.Weapon = _weapon = null;
            _locked = false;
        }


        public void Lock () {
            _locked = true;
        }


        public void LockAndUnequip () {
            if (_activeWorm.Worm != null) _activeWorm.Worm.Weapon = null;
            _weapon = null;
            _locked = true;
        }

    }

}
