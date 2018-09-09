using Geometry;
using UnityEngine;


namespace Battle.Camera {

    public class CameraWrapper : MonoBehaviour {

        private int _size;
        private CameraController _controller;

        [HideInInspector] public UnityEngine.Camera Camera;
        [HideInInspector] public Vector3 Target;


        public XY WorldMousePosition {
            get { return (Vector2) Camera.ScreenToWorldPoint(Input.mousePosition); }
        }


        public CameraController Controller {
            get {
                return _controller;
            }
            set {
                if (_controller != null) {
                    _controller.OnRemove ();
                }
                if (value != null) {
                    value.Camera = this;
                    value.OnAdd ();
                }
                _controller = value;
            }
        }


        private void Awake () {
            Camera = gameObject.GetComponent<UnityEngine.Camera>();
            Camera.orthographicSize = (_size = Screen.height) * 0.5f;
            Target = Camera.transform.position;
            Controller = new CameraController();
        }


        private void Update () {
            if (Screen.height != _size) Camera.orthographicSize = (_size = Screen.height) * 0.5f;
            Controller.Update();
            Camera.transform.position = Vector3.LerpUnclamped(Camera.transform.position, Target, 0.2f);
        }


        private void FixedUpdate () {
            //cameraWrapper.transform.position *= 0.8f;
            //cameraWrapper.transform.position += 0.2f * target;
        }


        public void LookAt (Vector2 xy, bool instantly = false) {
            Target.x = xy.x;
            Target.y = xy.y;
            if (instantly) Camera.transform.position = Target;
        }


        public void LookAt (Vector3 xyz, bool instantly = false) {
            LookAt((Vector2) xyz, instantly);
        }

    }

}
