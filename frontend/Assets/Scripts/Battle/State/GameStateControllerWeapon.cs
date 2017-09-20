using Battle.Weapons;
using Core;

namespace Battle.State {
    public partial class GameStateController
    {
        // todo: use arsenal
        private Weapon _weapon;
        public Weapon Weapon
        {
            get { return _weapon; }
            private set
            {
                _weapon = value;
                value.Equip(Worm);
            }
        }

        public int PreparedWeaponId { get; private set; }
        public bool CanSelectWeapon { get; private set; }

        public void PrepareWeapon(int id)
        {
            // if we can select weapon then arm active worm with it!
            PreparedWeaponId = id;
            //Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode(id); // 0 - select none
        }

        // method not used
        public void SelectWeapon(int id)
        {
            PreparedWeaponId = 0;
            Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode(PreparedWeaponId);
        }

        public void LockWeaponSelect()
        {
            CanSelectWeapon = false;
        }
    }
}