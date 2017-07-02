using UnityEngine;
using Utils;


namespace War.Camera {

    public class CameraController {

        protected CameraWrapper Camera;


        public CameraController (CameraWrapper camera) {
            this.Camera = camera;
        }


        public virtual void Update () {
            if (Input.GetMouseButtonDown(MouseButtons.Right)
                && !Input.GetMouseButtonUp(MouseButtons.Right)) {
                Camera.Controller = new MouseBasedCameraController(Camera);
            }
        }

    }

}
