using Battle.Weapons;
using Utils.Singleton;

namespace Battle.State
{
    public class WeaponWrapper
    {
        private int _preparedId;
        private Weapon _weapon;
        private bool _locked;

        public void Update(TurnData td)
        {
            if (!_locked && _preparedId != 0) SelectWeapon(_preparedId);
            _preparedId = 0;
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
            _preparedId = 0;
            _weapon = null;
        }
    }
}