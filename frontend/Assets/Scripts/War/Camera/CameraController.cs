using UnityEngine;
using Utils;


namespace War.Camera {

    public class CameraController {

        protected CameraWrapper camera;


        public CameraController (CameraWrapper camera) {
            this.camera = camera;
        }


        public virtual void Update () {
            if (Input.GetMouseButtonDown(MouseButtons.Right)
                && !Input.GetMouseButtonUp(MouseButtons.Right)) {
                camera.controller = new MouseBasedCameraController(camera);
            }
        }

    }

}
