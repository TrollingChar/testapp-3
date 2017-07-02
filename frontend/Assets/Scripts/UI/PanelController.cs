using UnityEngine;


namespace UI {

    public abstract class PanelController : MonoBehaviour {

        [SerializeField] protected Canvas Canvas;

        protected int CurrOpenness;
        protected int FullOpenness = 15;

        public bool Open;


        private void Start () {
            UpdatePosition();
        }


        private void Update () {
            if (Open) {
                if (CurrOpenness >= FullOpenness) return;
                ++CurrOpenness;
                UpdatePosition();
            } else {
                if (CurrOpenness <= 0) return;
                --CurrOpenness;
                UpdatePosition();
            }
        }


        protected abstract void UpdatePosition ();


        public void Show (bool instant = false) {
            Open = true;
            if (!instant) return;
            CurrOpenness = FullOpenness;
            UpdatePosition();
        }


        public void Hide (bool instant = false) {
            Open = false;
            if (!instant) return;
            CurrOpenness = 0;
            UpdatePosition();
        }


        public void Toggle (bool instant = false) {
            if (Open) {
                Hide(instant);
            } else {
                Show(instant);
            }
        }

    }

}
