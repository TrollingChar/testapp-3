using Geometry;


namespace Battle.Objects.Explosives {

    public abstract class Explosive : Component {

        public void Detonate () {
            Object.Despawn();
            OnDetonate(Object.Position);
        }


        public void Detonate (XY xy) {
            OnDetonate(xy);
        }


        protected abstract void OnDetonate (XY xy);

    }

}
