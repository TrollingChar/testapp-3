using War.Objects;


namespace War.CollisionHandlers {

    internal class CollisionHandler {

        private Object _obj;

        public virtual void OnAdd () {}
        public virtual void OnRemove () {}
        public virtual void PreCollision () {}
        public virtual void PostCollision () {}

    }

}
