using System;
using Battle.Objects;
using Battle.State;

namespace Battle.Weapons {

    public abstract class Weapon : Component {
        private GameStateController _state;

        public Worm Worm
        {
            get { return (Worm) Object; }
            set { Object = value; }
        }

        private Arsenals.Arsenal Arsenal {
            get { return Worm.Team.Arsenal; }
        }


        public abstract void Update (TurnData td);


        protected void UseAmmo () {
            Arsenal.UseAmmo(Id);
        }

        protected void LockArsenal ()
        {
            _state;
        }
    }

}
