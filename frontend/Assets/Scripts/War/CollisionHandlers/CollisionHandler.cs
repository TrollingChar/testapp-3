using War.Objects;


namespace War.CollisionHandlers {

    internal class CollisionHandler {

        private Object _obj;

        public void OnAdd () {}

        public void OnRemove () {}

        public virtual void PreCollision () {}

        public virtual void PostCollision () {}

    }

}
