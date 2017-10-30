namespace Battle.Objects.Explosives {

    public abstract class Explosive : Component {

        public void Detonate () {
            Object.Remove();
            OnDetonate();
        }


        protected abstract void OnDetonate ();


        public Explosive WithClusters (Object cluster, float coneAngle, float velocity, float velocityDelta) {
            // todo
            return this;
        }

    }

}
