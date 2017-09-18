namespace Battle.Objects {

    public abstract class Component/*<T> where T : Object*/ {

        public Object Object;

        public virtual void OnAdd () {}
        public virtual void OnRemove () {}

    }

}
