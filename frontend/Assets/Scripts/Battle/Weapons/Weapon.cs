using System;
using System.Linq;
using Attributes;
using Battle.Objects;
using Battle.State;

namespace Battle.Weapons {

    public abstract class Weapon : Component {
        private GameStateController _state;
        public int Id { get; private set; }
        
        public Worm Worm
        {
            get { return (Worm) Object; }
            set { Object = value; }
        }

        private Arsenals.Arsenal Arsenal {
            get { return Worm.Team.Arsenal; }
        }

        
        protected Weapon()
        {
            var attr = (WeaponAttribute) GetType().GetCustomAttributes(true).First(a => a is WeaponAttribute);
            Id = attr.Id;
        }


        public abstract void Update (TurnData td);


        protected void UseAmmo () {
            Arsenal.UseAmmo(Id);
        }


        protected void LockArsenal ()
        {
            _state.LockWeaponSelect();
        }
    }

}
