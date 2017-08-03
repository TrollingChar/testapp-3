using System;
using War.Teams;


namespace War.Weapons {

    public abstract class Weapon {

        private int _id;
        private bool _idSet;

        public int Id {
            get {
                if (!_idSet) throw new InvalidOperationException("Weapon id not set");
                return _id;
            }
            set {
                if (_idSet) throw new InvalidOperationException("Weapon id already set");
                _id = value;
                _idSet = true;
            }
        }


        public abstract void Update (TurnData td);


        protected void WasteAmmo () {
            Arsenal.WasteAmmo(Id);
        }


        private Arsenal Arsenal {
            get {
                // todo: it must be the arsenal from which this weapon will subtract ammo
                // and inject it via IOC
                return null;
            }
        }

    }

}
