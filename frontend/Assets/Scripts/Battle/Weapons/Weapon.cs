namespace Battle.Weapons {

    public abstract class Weapon {

        private Arsenals.Arsenal Arsenal {
            get {
                // todo: it must be the arsenal from which this weapon will subtract ammo
                return null;
            }
        }


        public abstract void Update (TurnData td);


        protected void WasteAmmo () {
//            Arsenal.WasteAmmo(Id);
        }

    }

}
