using System.Linq;
using Attributes;
using Battle.Arsenals;
using Battle.Objects;
using Battle.State;
using Utils.Singleton;

namespace Battle.Weapons {

    public abstract class Weapon : Component
    {
        private GameStateController _gameState;
        private int _id;
        
        public Worm Worm
        {
            get { return (Worm) Object; }
            set { Object = value; }
        }

        private Arsenal Arsenal {
            get { return Worm.Team.Arsenal; }
        }

        
        protected Weapon()
        {
            _gameState = The<GameStateController>.Get();
            _id = ((WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute)).Id;
        }


        public abstract void Update (TurnData td);


        protected void UseAmmo () {
            Arsenal.UseAmmo(_id);
        }


        protected void LockArsenal ()
        {
            GameState.LockWeaponSelect();
        }
    }

}
