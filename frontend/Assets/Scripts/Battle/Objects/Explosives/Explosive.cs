namespace Battle.Objects.Explosives {

    public abstract class Explosive : Component {

        public void Detonate () {
            Object.Despawn();
            OnDetonate();
        }


        protected abstract void OnDetonate ();

    }

}
