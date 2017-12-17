using Battle.Objects;
using Core;


namespace Battle.State {

    public class ActiveWorm {

        private Worm _worm;


        public ActiveWorm () {
            The.ActiveWorm = this;
        }


        public bool CanMove { get; set; }


        public Worm Worm {
            get { return _worm; }
            set {
                if (_worm != null) _worm.ArrowVisible = false;
                _worm = value;
                if (value == null) return;
                _worm.ArrowVisible = true;
            }
        }


        public bool Is (Worm worm) {
            return worm == _worm;
        }


        public void Set (Worm worm) {
            Worm = worm;
        }

    }

}
