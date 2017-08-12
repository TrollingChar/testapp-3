using UnityEngine;


namespace UI.Panels {

    public class Panel : MonoBehaviour {

        private PanelController _panelController;


        private void Awake () {
            _panelController = GetComponent<PanelController>();
            OnAwake();
        }


        private void Start () {
            Activate();
        }


        private void OnDestroy () {
            Deactivate();
        }


        protected virtual void OnAwake () {}
        protected virtual void Activate () {}
        protected virtual void Deactivate () {}


        public void Show (bool instantly = false) {
            _panelController.Show(instantly);
        }


        public void Hide (bool instantly = false) {
            _panelController.Hide(instantly);
        }

    }

}
