using Battle.Objects;

namespace Battle.State {
    public partial class GameStateController
    {
        private Worm _worm;
        public bool WormFrozen { get; private set; }

        public Worm Worm
        {
            get { return _worm; }
            private set
            {
                if (_worm != null) _worm.ArrowVisible = false;
                _worm = value;
                if (value == null) return;
                _worm.ArrowVisible = true;
            }
        }
    }
}