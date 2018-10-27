namespace Battle.Experimental {

    public abstract class NewGameState {

        public abstract void         Init ();
        public abstract NewGameState Next ();
        public abstract void         Update ();

    }

}