using Geometry;
using UnityEngine;


namespace War.Camera {

    public class CameraWrapper : MonoBehaviour {

        [HideInInspector] public UnityEngine.Camera Camera;
        [HideInInspector] public Vector3 Target;
        [HideInInspector] public CameraController Controller;

        private int _size;


        private void Awake () {
            Camera = gameObject.GetComponent<UnityEngine.Camera>();
            Camera.orthographicSize = (_size = Screen.height) * 0.5f;
            Target = Camera.transform.position;
            Controller = new CameraController(this);
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


        public void LookAt (Vector3 xyz, bool instantly = false) {
            Target.x = xyz.x;
            Target.y = xyz.y;
            if (instantly) Camera.transform.position = Target;
        }


        public XY WorldMousePosition {
            get { return (Vector2) (Camera.ScreenToWorldPoint(Input.mousePosition)); }
        }

    }

}
