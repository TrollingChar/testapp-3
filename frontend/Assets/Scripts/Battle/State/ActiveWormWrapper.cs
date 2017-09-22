using Battle.Objects;

namespace Battle.State
{
    public class ActiveWormWrapper
    {
        private Worm _worm;
        private bool _canMove;

        
        public Worm Worm {
            get { return _worm; }
            set {
                if (_worm != null) _worm.ArrowVisible = false;
                _worm = value;
                if (value == null) return;
                _worm.ArrowVisible = true;
            }
        }

        
        public bool Is(Worm worm) {
            return worm == _worm;
        }


        public void Set(Worm worm)
        {
            _worm = worm;
        }
    }
}