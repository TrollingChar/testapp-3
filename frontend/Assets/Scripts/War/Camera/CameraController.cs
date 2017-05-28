using UnityEngine;

namespace W3 {
    public class CameraController {
        protected CameraWrapper camera;

        public CameraController (CameraWrapper camera) {
            this.camera = camera;
        }

        virtual public void Update () {
            if (Input.GetMouseButtonDown(MouseButtons.Right)
            && !Input.GetMouseButtonUp(MouseButtons.Right)) {
                camera.controller = new MouseBasedCameraController(camera);
            }
        }
    }
}