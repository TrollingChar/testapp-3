using Battle.Weapons;
using Utils.Singleton;

namespace Battle.State
{
    public class WeaponWrapper
    {
        public int PreparedId { get; set; }
        private Weapon _weapon;
        private bool _locked;

        public void Update(TurnData td)
        {
            if (!_locked && PreparedId != 0) SelectWeapon(PreparedId);
            PreparedId = 0;
            if (_weapon != null) _weapon.Update(td);
        }

        private void SelectWeapon(int weaponId)
        {
            
        }

        public WeaponWrapper()
        {
            The<WeaponWrapper>.Set(this);
        }

        public void OnNewTurn()
        {
            PreparedId = 0;
            _weapon = null;
            _locked = false;
        }

        public void LockSelect()
        {
            _locked = true;
        }
    }
}