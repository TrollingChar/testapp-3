using System;
using Battle.Weapons;
using DataTransfer.Data;


namespace Battle.Experimental {

    public partial class BattleScene {

        private bool ArsenalLocked { get; set; }


        private void UpdateWeapon (TurnData td) {
            if (ActiveWorm == null) return;
            // if (!_locked && td != null && td.Weapon != 0) Select(td.Weapon);
            if (!ArsenalLocked && td.Weapon != 0) ActiveWorm.Weapon = Weapon.ById (td.Weapon);
            ActiveWorm.UpdateWeapon (td);
        }


        public void UnlockArsenal () {
            ArsenalLocked = false;
            ActiveWorm.Weapon = null;
            TurnData.PrepareNoWeapon ();
        }


        public void LockArsenal () {
            ArsenalLocked = true;
        }


        public void OnWeaponClicked (int weaponId) {
            TurnData.PrepareWeapon ((byte) weaponId);
        }

    }

}