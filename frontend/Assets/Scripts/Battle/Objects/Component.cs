namespace Battle.Objects {

    public abstract class Component {

        public Object Object;

        public virtual void OnAdd () {}
        public virtual void OnRemove () {}

    }

}
