using Geometry;
using UnityEngine;


namespace War.Camera {

    public class CameraWrapper : MonoBehaviour {

        [HideInInspector] public new UnityEngine.Camera camera;
        [HideInInspector] public Vector3 target;
        [HideInInspector] public CameraController controller;

        private int size = 0;


        private void Awake () {
            camera = gameObject.GetComponent<UnityEngine.Camera>();
            camera.orthographicSize = (size = Screen.height) * 0.5f;
            target = camera.transform.position;
            controller = new CameraController(this);
        }


        private void Update () {
            if (Screen.height != size) camera.orthographicSize = (size = Screen.height) * 0.5f;
            controller.Update();
            camera.transform.position = Vector3.LerpUnclamped(camera.transform.position, target, 0.2f);
        }


        private void FixedUpdate () {
            //cameraWrapper.transform.position *= 0.8f;
            //cameraWrapper.transform.position += 0.2f * target;
        }


        public void LookAt (Vector3 xyz, bool instantly = false) {
            target.x = xyz.x;
            target.y = xyz.y;
            if (instantly) camera.transform.position = target;
        }


        public XY worldMousePosition {
            get { return (Vector2) (camera.ScreenToWorldPoint(Input.mousePosition)); }
        }

    }

}
