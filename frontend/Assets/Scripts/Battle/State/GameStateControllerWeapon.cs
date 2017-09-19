using Battle.Weapons;
using Core;

namespace Battle.State {
    public partial class GameStateController
    {
        public Weapon Weapon { get; private set; }
        public int PreparedWeaponId { get; private set; }

        public void PrepareWeapon(int id)
        {
            // if we can select weapon then arm active worm with it!
            PreparedWeaponId = id;
            //Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode(id); // 0 - select none
        }

        public void SelectWeapon(int id)
        {
            PreparedWeaponId = 0;
            Weapon = id == 0 ? null : Serialization<Weapon>.GetNewInstanceByCode(PreparedWeaponId);
        }
    }
}