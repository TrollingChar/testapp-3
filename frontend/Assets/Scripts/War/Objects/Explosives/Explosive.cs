namespace War.Objects.Explosives {

    public class Explosive : Component {

        public virtual void Detonate () {
            Object.Remove();
        }

    }

}
