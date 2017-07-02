using UnityEngine;


namespace UI {

    public abstract class PanelController : MonoBehaviour {

        protected int currOpenness = 0;
        protected int fullOpenness = 15;
        public Canvas canvas;
        public bool open;


        private void Start () {
            UpdatePosition();
        }


        private void Update () {
            if (open) {
                if (currOpenness >= fullOpenness) return;
                ++currOpenness;
                UpdatePosition();
            } else {
                if (currOpenness <= 0) return;
                --currOpenness;
                UpdatePosition();
            }
        }


        protected abstract void UpdatePosition ();


        public void Show (bool instant = false) {
            open = true;
            if (!instant) return;
            currOpenness = fullOpenness;
            UpdatePosition();
        }


        public void Hide (bool instant = false) {
            open = false;
            if (!instant) return;
            currOpenness = 0;
            UpdatePosition();
        }


        public void Toggle (bool instant = false) {
            if (open) {
                Hide(instant);
            } else {
                Show(instant);
            }
        }

    }

}
